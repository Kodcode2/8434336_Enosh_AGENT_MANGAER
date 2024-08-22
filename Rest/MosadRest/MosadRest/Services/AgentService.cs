using Microsoft.EntityFrameworkCore;
using MosadRest.Data;
using MosadRest.DtoModels;
using MosadRest.Models;
using System.Reflection;

namespace MosadRest.Services
{
    public class AgentService(ApplicationDbContext _DbContext) : IAgentService
    {
        public async Task<ResIdDto?> CreateAgentasync(AgentDto agentDto)
        {
            AgentModel newAgent = new()
            {
                NickName = agentDto.nickname,
                PhotoUrl = agentDto.photo_url
            };
            await _DbContext.AddAsync(newAgent);
            await _DbContext.SaveChangesAsync();
            return new()
            {
                Id = newAgent.Id,
            }; 
        }
        public async Task<List<AgentModel>> GetAllAgentsAsync()
        {
            return  await _DbContext.Agents.ToListAsync() ?? new();
        }  
        public async Task<bool> PinAgentAsync (int id, locationDto location)
        {
            AgentModel? agent = await _DbContext.Agents.FindAsync(id);
            if (agent == null)
                throw new Exception("Not Found");
            if (agent.XWaypoint == -201 || agent.YWaypoint == -201)
            {
                agent.XWaypoint = location.x;
                agent.YWaypoint = location.y;
                if (!IsLocationValid(agent.XWaypoint, agent.YWaypoint))
                    throw new Exception("InValid Location");
                await _DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }       
        public async Task<bool> MoveAgentAsync(AgentModel agent, DirectionDto directionDto)
        {
            var numDirection = directionDto.NumDirection[directionDto.direction];
            agent.XWaypoint += numDirection.Value.x;
            agent.YWaypoint += numDirection.Value.y;
            if (IsLocationValid(agent.XWaypoint, agent.YWaypoint))
            {
                await _DbContext.SaveChangesAsync();
                return true;
            }
            throw new Exception("InValid Location");
        }
        public bool IsLocationValid(int x, int y) =>
             x >= 0 && x < 1000 && y >= 0 && y < 1000;
        public async Task<List<MissionModel>?> ChkAndCreateMissins(AgentModel agent)
        {
            List<MissionModel>? missions = await GetOffersByAgentId(agent.Id);
            if (missions != null)
            {
                //missions = await missions.Where(async x => IsInKillZone(agent, 
                    //await _DbContext.Targets.FindAsync(x.TargetId))).ToListAsync();
            }





            return new List<MissionModel>();
        }
        public async Task<List<MissionModel>?> GetOffersByAgentId(int id) =>
                 await _DbContext.Missions.Where(x => x.AgentId == id)
                .Where(x => x.MissionStatus == MissionStatus.offer).ToListAsync();
        public bool IsInKillZone(AgentModel agent, TargetModel target)
        {
            return Math.Sqrt(Math.Pow(agent.XWaypoint - target.XWaypoint, 2)
                + Math.Pow(agent.YWaypoint - target.YWaypoint, 2)) <= 200;
        }
        public bool IsAgentActive(AgentModel agent)
            => agent.Status == AgentStatus.Active;
        public async Task<AgentModel?> GetAgentByIdAsync(int id)
        {
            return await _DbContext.Agents.FindAsync(id);
        }
    }
}
