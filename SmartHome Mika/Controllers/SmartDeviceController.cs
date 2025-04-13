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

        public IActionResult Zoeken(string zoekterm)
        {
            Database db = new();
            List<SmartDevice> resultaten = db.SearchDevicesByName(zoekterm);

            return View(resultaten); 
        }

        [HttpPost]
        public IActionResult ChangeIsOn(int Id, string DeviceType)
        {
            Database db = new();

            db.ChangeIsOn(Id, DeviceType);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult AddDevice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDevice(SmartDevice device, string deviceType)
        {
            Database db = new();
            db.AddDevice(device, deviceType);

            return RedirectToAction("Index", "Home");
        }


    }
}
