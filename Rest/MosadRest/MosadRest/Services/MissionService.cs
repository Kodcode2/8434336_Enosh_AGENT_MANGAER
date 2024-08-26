using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MosadRest.Data;
using MosadRest.DtoModels;
using MosadRest.Models;
using System.Reflection;

namespace MosadRest.Services
{
    /*    public class MissionService(IAgentService agentService, ITargetService targetService,
            ApplicationDbContext _DbContext) : IMissionService*/

    public class MissionService(ApplicationDbContext _DbContext,
   IServiceProvider serviceProvider
) : IMissionService
    {
        private IAgentService agentService = serviceProvider.GetRequiredService<IAgentService>();
        private ITargetService targetService = serviceProvider.GetRequiredService<ITargetService>();

        public async Task ChkAndCreateMissinsForAgentAsync(AgentModel agent)
        {
            var targets = await _DbContext.Targets.ToListAsync();
            foreach (var target in targets)
            {
                ChkAndCreateMissinsAsync(target, agent);
            }
        }
        public async Task ConfromandEditMussunsForAgentAsinc(AgentModel agent)
        {
            List<MissionModel> missions = await _DbContext.Missions.Include(m => m.Target).ToListAsync();

            List<MissionModel> offerMissions = GetOffersByAgentIdAsync(agent.Id, missions);

            List<MissionModel> updatedMissions = offerMissions
                .Where(m => IsInKillZone(agent, m.Target) && !m.Target.IsHunted)
                .Select(m =>
                {
                    var updatedMission = _DbContext.Attach(m).Entity;
                    updatedMission.AgentId = agent.Id;
                    updatedMission.TimeLeft = TimeLeftMuusion(agent, m.Target);
                    _DbContext.Entry(updatedMission).State = EntityState.Modified;
                    return updatedMission;
                })
                .ToList();

            if (updatedMissions.Any())
            {
                await _DbContext.SaveChangesAsync();
            }

            List<MissionModel> missionsToDelete = offerMissions
                    .Where(m => !IsInKillZone(agent, m.Target) || m.Target.IsHunted)
                    .ToList();

            if (missionsToDelete.Any())
            {
                _DbContext.Missions.RemoveRange(missionsToDelete);
                await _DbContext.SaveChangesAsync();

            }

        }
        public async Task ChkAndCreateMissinsForTargetAsync(TargetModel target)
        {
            var agents = await _DbContext.Agents.ToListAsync();
            foreach (var agent in agents)
            {
                ChkAndCreateMissinsAsync(target, agent);
            }
        }
        public async Task ChkAndCreateMissinsAsync(TargetModel target, AgentModel agent)
        {
            if (_DbContext.Missions.FirstOrDefault(m => m.AgentId == agent.Id && m.TargetId == target.Id) != null)
                return;
            if (target.Status == TargetStatus.live
                && target.IsHunted == false
                && agent.Status == AgentStatus.InActive
                && IsInKillZone(agent, target))
                _DbContext.Add(new MissionModel()
                {
                    AgentId = agent.Id,
                    TargetId = target.Id,
                    TimeLeft = TimeLeftMuusion(agent, target),
                    MissionStatus = MissionStatus.offer,
                    _StartTime = DateTime.Now,
                });
            _DbContext.SaveChanges();
        }
        public async Task ConfromandEditMussunsForTargetAsinc(TargetModel target)
        {
            List<MissionModel?> missinons = await GetOffersByTargetIdAsync(target.Id);
            if (missinons != null)
                foreach (var mission in missinons)
                {
                    var agent = await GetAgentByMissonAsync(mission);
                    //בודק האם יש משימה מצוותת על המטרה
                    if (target.IsHunted && mission.MissionStatus == MissionStatus.team)
                    {
                        //ואם כן מעדכן את הזמן הנותר
                        if (IsKilingDan(agent, target))
                            UpdateMiisionByMission(mission);
                        break;
                    }
                    //בודק שהסוכן עדיין פנוי והמשימה עדיין בטווח
                    if (IsInKillZone(agent, target) && !target.IsHunted)
                    {
                        mission.TimeLeft = TimeLeftMuusion(agent, target);
                        continue;
                    }
                    missinons.Remove(mission);
                }
            await _DbContext.SaveChangesAsync();
        }

        public async Task<List<MissionModel>> GetOffersByTargetIdAsync(int id)
        {
            return await _DbContext.Missions.Where(x => x.TargetId == id)
                .Where(x => x.MissionStatus == MissionStatus.offer).ToListAsync();
        }
        public List<MissionModel> GetOffersByAgentIdAsync(int id, List<MissionModel> missions) =>
             missions
                .Where(x => x.AgentId == id)
                .Where(x => x.MissionStatus == MissionStatus.offer).ToList() ?? [];

        public double TimeLeftMuusion(AgentModel agent, TargetModel target)
        {
            return Math.Sqrt(Math.Pow(agent.XWaypoint - target.XWaypoint, 2)
                + Math.Pow(agent.YWaypoint - target.YWaypoint, 2)) / 5;
        }
        public bool IsInKillZone(AgentModel agent, TargetModel target)
        {
            return Math.Sqrt(Math.Pow(agent.XWaypoint - target.XWaypoint, 2)
                + Math.Pow(agent.YWaypoint - target.YWaypoint, 2)) <= 200;
        }


        public async Task<AgentModel> GetAgentByMissonAsync(MissionModel mission)
        {
            return await _DbContext.Agents.FindAsync(mission.AgentId);
        }
        public async Task<TargetModel> GetTargetByMissonAsync(MissionModel mission)
        {
            return await _DbContext.Targets.FindAsync(mission.TargetId);
        }

        public async Task<MissionModel> GetMissionByIdAsync(int id)
        {
            return await _DbContext.Missions.FindAsync(id);
        }

        public async Task<List<MissionModel>> GetAllMisionsasync()
        {
            var res = await _DbContext.Missions.ToListAsync();
            return res;
        }

        public async Task UpdteMission()
        {
            var missions = _DbContext.Missions.Where(m => m.MissionStatus == MissionStatus.team).ToList();
            foreach (var miision in missions)
            {
                UpdateMiisionByMission(miision);
            }
            _DbContext.SaveChanges();
        }
        public async Task UpdateMiisionByMission(MissionModel mission)
        {
            {
                AgentModel agent = _DbContext.Agents.Find(mission.AgentId);
                TargetModel target = _DbContext.Targets.Find(mission.TargetId);
                agentService.AgentAtakToKil(agent, target);
                if (IsKilingDan(agent, target))
                {
                    target.Status = TargetStatus.dead;
                    agent.Status = AgentStatus.InActive;
                    mission.MissionStatus = MissionStatus.finished;
                    mission.TotalExecutionTime = (DateTime.Now -mission._StartTime).TotalHours;
                }
                mission.TimeLeft = TimeLeftMuusion(agent, target);
                _DbContext.SaveChanges();

            }
        }

        public async Task AssimntMission(int id)
        {
            MissionModel? mission = _DbContext.Missions.Find(id);
            if (mission == null)
                throw new Exception("mission aborted");
            AgentModel agent = _DbContext.Agents.Find(mission.AgentId);
            TargetModel target = _DbContext.Targets.Find(mission.TargetId);
            mission.MissionStatus = MissionStatus.team;
            agent.Status = AgentStatus.Active;
            target.IsHunted = true;
            ConfromandEditMussunsForTargetAsinc(target);
            ConfromandEditMussunsForAgentAsinc(agent);
            await _DbContext.SaveChangesAsync();

        }
        public bool IsKilingDan(AgentModel agent, TargetModel target)
        {
            return agent.XWaypoint == target.XWaypoint && agent.YWaypoint == target.YWaypoint;
        }

        public MissionModel GetDetailsById(int id)
        {
            var mission = _DbContext.Missions.Include(m => m.Agent)
                .Include(m => m.Target)
                .FirstOrDefault(m => m.Id == id);
            return mission ?? null;

        }
    }
}
