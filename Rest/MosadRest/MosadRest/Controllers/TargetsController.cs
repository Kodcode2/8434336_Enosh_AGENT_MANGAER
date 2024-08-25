using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MosadRest.DtoModels;
using MosadRest.Models;
using MosadRest.Services;

namespace MosadRest.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TargetsController(ITargetService targetService, IMissionService missionService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateAgentAsync([FromBody] TargetDto targetDto)
        {
            return Ok(await targetService.CreateTargetAsync(targetDto));
        }
        [HttpGet]
        public async Task<ActionResult> GetAllTargetAsync()
        {
            var res = await targetService.GetAllTargets();
            return Ok(res);
        }
        [HttpPut("{id}/pin")]
        public async Task<ActionResult> PinTargetAsync(int id, [FromBody] locationDto locationDto)
        {     
            try
            {
                await targetService.PinTargetAsync(id, locationDto);
                TargetModel target =  await targetService.GetTargetById(id);
                await missionService.ChkAndCreateMissinsForTargetAsync(target);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/move")]
        public async Task<ActionResult> MoveAgentAsync(int id, [FromBody] DirectionDto directionDto)
        {
            try
            {
                await targetService.MoveTargetAsync(id, directionDto);
                TargetModel target = await targetService.GetTargetById(id);
                await missionService.ConfromandEditMussunsForTargetAsinc(target);
                await missionService.ChkAndCreateMissinsForTargetAsync(target);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

    }
}




