using Microsoft.EntityFrameworkCore;
using MosadRest.Data;
using MosadRest.DtoModels;
using MosadRest.Models;
using MosadRest.Utils;
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
                PhotoUrl = agentDto.photoUrl
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
        public async Task PinAgentAsync (AgentModel agent, locationDto location)
        {
            try
            {
                agent.XWaypoint = location.x;
                agent.YWaypoint = location.y;
                if (!AgentTargetUtils.IsLocationValid(agent.XWaypoint, agent.YWaypoint))
                    throw new Exception("InValid Location");                       
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            await _DbContext.SaveChangesAsync();
        }       
        public async Task MoveAgentAsync(AgentModel agent, DirectionDto directionDto)
        {
            var numDirection = directionDto.NumDirection[directionDto.direction];
            try
            {
                AgentTargetUtils.MuveAgentStep(agent, 
                    numDirection.Value.x, numDirection.Value.y);
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public bool IsAgentActive(AgentModel agent)
            => agent.Status == AgentStatus.Active;
        public async Task<AgentModel?> GetAgentByIdAsync(int id)
        {
            return await _DbContext.Agents.FindAsync(id) ?? 
                throw new Exception ("Not found");
        }

        public void AgentAtakToKil(AgentModel agent, TargetModel target)
        {
            if (agent.XWaypoint != target.XWaypoint)
                agent.XWaypoint = agent.XWaypoint < target.XWaypoint ? agent.XWaypoint + 1 : agent.XWaypoint - 1;
            if (agent.YWaypoint != target.YWaypoint)
                agent.YWaypoint = agent.YWaypoint < target.YWaypoint ? agent.YWaypoint + 1 : agent.YWaypoint - 1;

        }
    }
}
