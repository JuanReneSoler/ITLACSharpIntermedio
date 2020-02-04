using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tareas.Models;

namespace Tareas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Route"] = new List<HomebuttonsModel>{
                new HomebuttonsModel{Text = "Home", ActionName="Home" }
            };
            ViewData["Buttons"] = new List<HomebuttonsModel>{
                new HomebuttonsModel{ Text = "Tarea 1", ActionName = "Tarea1"},
                new HomebuttonsModel{Text="Tarea 2(Tabla Periodica)", ActionName="Tarea2"}
            };
            return View();
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
}
