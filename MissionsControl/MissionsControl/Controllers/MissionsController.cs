using Microsoft.AspNetCore.Mvc;
using MissionsControl.Services;

namespace MissionsControl.Controllers
{
    public class MissionsController(IMissionsService missionsService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var res = await missionsService.GetAllMissionsAsync();
            return View(res);           
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var res = await missionsService.GetMissionDetailsById(id);
            if (res != null)
                return View(res);
            return RedirectToAction("Index", "Home");
            
        }
    }
}
