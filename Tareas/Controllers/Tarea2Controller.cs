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
    public class Tarea2Controller: Controller
    {
        public IActionResult Index()
        {
            ViewData["Route"] = new List<HomebuttonsModel>{
                new HomebuttonsModel{Text="Home", ActionName = "Home"},
                new HomebuttonsModel{Text="Tarea 2", ActionName = "Tarea2"}
            };
            return View();
        }
    }
}