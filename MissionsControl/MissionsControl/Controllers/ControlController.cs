using Microsoft.AspNetCore.Mvc;
using MissionsControl.Services;

namespace MissionsControl.Controllers
{
    public class ControlController(IControlService controlService) : Controller
    {
        public IActionResult Index()
        {
            var res = controlService.GetGeneralInfo();
            if (res != null) 
                return View(res);
             return RedirectToAction("Index","Home");
        }
    }
}
