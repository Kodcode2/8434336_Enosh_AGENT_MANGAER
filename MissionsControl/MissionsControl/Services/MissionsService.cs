using MissionsControl.DtoModels;
using MissionsControl.Models;
using MissionsControl.ViewModels;
using System;
using System.Text.Json;

namespace MissionsControl.Services
{
    public class MissionsService(IHttpClientFactory clientFactory) : IMissionsService
    {
        private readonly string _url = "https://localhost:7077/missions";

        public async Task<List<MissionDto>?> GetAllMissionsAsync()
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _url);
            var res = await httpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                List<MissionDto?> missions = JsonSerializer.Deserialize<List<MissionDto>>(
                    content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).ToList();
                return missions;
            }
            return null;
        }

        public async Task<MissionFullDetailsVm> GetMissionDetailsById(int id)
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/details/{id}" );
            var res = await httpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                MissionFullDetailsDto? mission = JsonSerializer.Deserialize<MissionFullDetailsDto>(
                    content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return new()
                {
                    Id = mission.Id,
                    AgentId = mission.AgentId,
                    NickName = mission.Agent.NickName,
                    AgentXWaypoint = mission.Agent.XWaypoint,
                    AgentYWaypoint = mission.Agent.YWaypoint,
                    TargetId = mission.TargetId,
                    Name = mission.Target.Name,
                    TargetXWaypoint = mission.Target.XWaypoint,
                    TargetYWaypoint = mission.Target.YWaypoint,
                    TimeLeft = mission.TimeLeft,
                    Distans = mission.TimeLeft * 5
                };
            }
            return null;
        }
        public async Task<List<MissionFullDetailsDto>> GetAllMissionsFullAsync()
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/details");
            var res = await httpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                List<MissionFullDetailsDto?> missions = JsonSerializer.Deserialize<List<MissionFullDetailsDto>>(
                    content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return missions ?? new();
            }
            return null;
        }
    }
    
}
/*
details

        public int? TargetId { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public int TargetXWaypoint { get; set; }
        public int TargetYWaypoint { get; set; }
        public double? TimeLeft { get; set; }
        public double? Distans { get; set; }

var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url + "/getById" + id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authentication.Token);
            var res = await httpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                UserVM? user = JsonSerializer.Deserialize<UserVM>(
                    content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return user;
            }

*/