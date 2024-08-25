using MosadRest.DtoModels;
using MosadRest.Models;

namespace MosadRest.Services
{
    public interface IAgentService
    {
        public Task<ResIdDto?> CreateAgentasync(AgentDto agentDto);
        
        public Task<List<AgentModel>?> GetAllAgentsAsync();
        public Task PinAgentAsync(AgentModel agent, locationDto location);
        public Task  MoveAgentAsync(AgentModel agent, DirectionDto directionDto);
        public bool IsAgentActive(AgentModel agent);
        public Task<AgentModel?> GetAgentByIdAsync(int id);
        public void AgentAtakToKil(AgentModel agent, TargetModel target);

    }
}



