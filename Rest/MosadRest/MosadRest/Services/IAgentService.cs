using MosadRest.DtoModels;
using MosadRest.Models;

namespace MosadRest.Services
{
    public interface IAgentService
    {
        public Task<ResIdDto?> CreateAgent(AgentDto agentDto);
        public Task<ResIdDto?> CreateTarget(TargetDto targetDto);
        public Task<List<TargetModel>?> GetAllTargets();
        public Task<List<AgentModel>?> GetAllAgents();
        public Task<bool> PinTarget(int id,locationDto location);
        public Task<bool> PinAgent(int id,locationDto location);
        public Task<bool> MoveTarget(int id, DirectionDto direction);
        public Task<bool> MoveAgent(int id, DirectionDto direction);
    }
}
