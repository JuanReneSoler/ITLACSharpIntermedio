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
            ViewData["Buttons"] = new List<HomeButtonsRequest>{
                new HomeButtonsRequest{ Text = "Tarea 1", ControllerName = "Tarea1", ActionName = "Index"},
                new HomeButtonsRequest{Text="Tarea 2(Tabla Periodica)", ControllerName="Tarea2", ActionName = "Index"},
                new HomeButtonsRequest{Text="Tarea 3 (Matenimiento)", ControllerName="Tarea3", ActionName="Index"},
                new HomeButtonsRequest{Text="Tarea 4 (Pkedex)", ControllerName="Tarea4", ActionName = "IndexPokemon"}
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
