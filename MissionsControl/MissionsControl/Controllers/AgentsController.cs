using Microsoft.AspNetCore.Mvc;
using MissionsControl.Services;

namespace MissionsControl.Controllers
{
    public class AgentsController(IAgentsService agentService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var res =await agentService.GetAgentAsync();
            if (res != null) 
                return View(res);
            return RedirectToAction("Index", "Home");
        }
    }
}
