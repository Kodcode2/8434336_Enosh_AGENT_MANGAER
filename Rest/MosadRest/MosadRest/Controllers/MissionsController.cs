using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MosadRest.Models;
using MosadRest.Services;

namespace MosadRest.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MissionsController(IMissionService missionService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetMissions()
        {
            var res = await missionService.GetAllMisionsasync();
            return Ok(res);
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateMission()
        {
            await missionService.UpdteMission();
            return Ok();
        }

        [HttpPut("{id}")]
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
        [HttpGet("details/{id}")]
        public ActionResult GetDetails(int id)
        {
                var res = missionService.GetDetailsById(id);
            if (res != null)
                return Ok(res);
            return NotFound("This Mission Dasnot Exsist");
            
            
        }
        [HttpGet("details")]
        public ActionResult GetAllDetails()
        {
            var res = missionService.GetAllDetails();
            if (res != null)
                return Ok(res);
            return NotFound("This Mission Dasnot Exsist");
        }
    }

}
//“status”:““status”:“assigned””