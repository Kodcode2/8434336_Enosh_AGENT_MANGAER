using MosadRest.Data;
using MosadRest.DtoModels;
using MosadRest.Models;

namespace MosadRest.Services
{
    public class AgentService(ApplicationDbContext _DbContext) : IAgentService
    {
        public async Task<ResIdDto?> CreateAgent(AgentDto agentDto)
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

        public async Task<ResIdDto?> CreateTarget(TargetDto targetDto)
        {
            TargetModel newTarget = new()
            {
                Name = targetDto.name,
                Position = targetDto.position,
                PhotoUrl = targetDto.photo_url
            };
            await _DbContext.AddAsync(newTarget);
            await _DbContext.SaveChangesAsync();
            return (new()
            {
                Id = newTarget.Id,
            });
        }

        public async Task<List<AgentModel>> GetAllAgents()
        {
            return  _DbContext.Agents.ToList() ?? new();
        }

        public async Task<List<TargetModel>> GetAllTargets()
        {
            return _DbContext.Targets.ToList() ?? new();
        }

        public async Task<bool> PinTarget(int id, locationDto location)
        {
            TargetModel? target = await _DbContext.Targets.FindAsync(id);
            if (target == null)
                throw new Exception("Not Found");
            if (target.Status != TargetModel.TargetStatus.live)
                return false;
            if (target.XWaypoint == 201 || target.YWaypoint == 201)
            {
                target.XWaypoint = location.x;
                target.YWaypoint = location.y;
                await _DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> PinAgent (int id, locationDto location)
        {
            AgentModel? agent = await _DbContext.Agents.FindAsync(id);
            if (agent == null)
                throw new Exception("Not Found");
            if (agent.Status != AgentStatus.Active)
                return false;
            if (agent.XWaypoint == 201 || agent.YWaypoint == 201)
            {
                agent.XWaypoint = location.x;
                agent.YWaypoint = location.y;
                await _DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
