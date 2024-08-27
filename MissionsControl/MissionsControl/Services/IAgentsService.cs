using MissionsControl.DtoModels;

namespace MissionsControl.Services
{
    public interface IAgentsService
    {
        public Task<List<AgentDto>> GetAgentAsync();
    }
}
