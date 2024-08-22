using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MosadRest.DtoModels;
using MosadRest.Models;
using MosadRest.Services;

namespace MosadRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentsController(IAgentService agentService) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> CreateAgentAsync([FromBody] AgentDto agentDto)
        {
            return Ok(await agentService.CreateAgentasync(agentDto));
        }
        [HttpGet]
        public async Task<ActionResult> GetAllAgentsAsync()
        {
            var res = await agentService.GetAllAgentsAsync();
            return Ok(res);
        }
        [HttpPut("{id}/pin")]
        public async Task<ActionResult> PinAgentAsync(int id, [FromBody] locationDto locationDto)
        {
            AgentModel? agent = await agentService.GetAgentByIdAsync(id);
            if (agentService.IsAgentActive(agent))
                return BadRequest("The agent is active");
            try
            {
                return await agentService.PinAgentAsync(id, locationDto) ? Ok() : BadRequest("didint work");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}/move")]
        public async Task<ActionResult> MoveAgentAsync(int id, [FromBody]  DirectionDto directionDto)
        {
            AgentModel? agent = await agentService.GetAgentByIdAsync(id);
            if (agentService.IsAgentActive(agent))
                return BadRequest("The agent is active");
            try
            {
                return Ok(await agentService.MoveAgentAsync(agent, directionDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
