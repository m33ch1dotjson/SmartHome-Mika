using Microsoft.AspNetCore.Mvc;
using SmartHome_Mika.Models;


namespace SmartHome_Mika.Controllers
{
    public class SmartDeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeIsOn(int Id, string DeviceType)
        {
            Database db = new();

            db.ChangeIsOn(Id, DeviceType);

            return RedirectToAction("Index", "Home");

        }

    }
}
