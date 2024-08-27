using Microsoft.AspNetCore.Mvc;
using MissionsControl.DtoModels;
using MissionsControl.ViewModels;
using System.Security.Policy;
using System.Text.Json;

namespace MissionsControl.Services
{
    public class ControlService(IHttpClientFactory clientFactory,IMissionsService missionsService) : IControlService
    {
        public async Task<GeneralInfoDto> GetGeneralInfo()
        {
            var misons = await missionsService.GetAllMissionsFullAsync();
            if (misons == null)
                return null;
            var res =  new GeneralInfoDto()
            {
                AmountAgents = misons.Select(x => x.AgentId).ToHashSet().Count(),
                AmountAgentsActivity = misons.Select(x => x.Agent).
                Where(x => x.Status == AgentStatus.Active).ToHashSet().Count(),
                AmountTargets = misons.Select(x => x.TargetId).ToHashSet().Count(),
                AmountTargetsKild = misons.Select(x => x.Target).
                Where(x => x.Status == TargetStatus.dead).ToHashSet().Count(),
                AmountMissions = misons.Count(),
                AmountMissionsActivity = misons.Where(m => m.MissionStatus == MissionStatus.team).Count(),
                RelationAgentsTargetsTeamable = misons.Where(m => m.MissionStatus == MissionStatus.offer).Select(x => x.AgentId).ToHashSet().Count()
                / misons.Where(m => m.MissionStatus == MissionStatus.offer).Select(x => x.TargetId).ToHashSet().Count()
            };
            res.RelationAgentsTargets = res.AmountAgents / res.AmountTargets;
            return res;
        }

    }
}
