using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartHome_Mika.Models;
using SmartHome_Mika.Services;

namespace SmartHome_Mika.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Database db = new();
        SmartLamp lamp = (SmartLamp)db.GetDeviceById(1, "SmartLamp");
        SmartFridge fridge = (SmartFridge)db.GetDeviceById(1, "SmartFridge");

        HomeViewModel model = new HomeViewModel
        {
            Lamp = lamp,
            Fridge = fridge
        };

        return View(model);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
