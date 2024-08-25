using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MosadRest.Models;
using MosadRest.Services;

namespace MosadRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController(IMissionService missionService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetMissions()
        {
            return Ok(await missionService.GetAllMisionsasync());
        }

        [HttpPost("/update")]
        public async Task<ActionResult> UpdateMission()
        {
            await missionService.UpdteMission();
            return Ok();
        }

        [HttpPut("/{id}")]
        public async Task<ActionResult> MissionAssigned(int id)
        {
            try
            {
                await missionService.AssimntMission(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
//“status”:““status”:“assigned””