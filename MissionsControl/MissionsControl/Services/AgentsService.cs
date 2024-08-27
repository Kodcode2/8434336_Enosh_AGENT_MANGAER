using MissionsControl.DtoModels;
using MissionsControl.Models;
using System.Security.Policy;
using System.Text.Json;

namespace MissionsControl.Services
{
    public class AgentsService(IHttpClientFactory clientFactory) : IAgentsService
    {
        private readonly string _url = "https://localhost:7077/agents";
        public async Task<List<AgentDto>> GetAgentAsync()
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _url);
            var res = await httpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                List<AgentDto?> missions = JsonSerializer.Deserialize<List<AgentDto>>(
                    content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).ToList();
                return missions;
            }
            return null;
        }
    }
}

