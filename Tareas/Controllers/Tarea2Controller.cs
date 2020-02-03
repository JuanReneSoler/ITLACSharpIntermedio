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
            ViewData["Route"] = new List<string>{
                "Home",
                "Tarea 2"
            };
            return View();
        }
    }
}