using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tareas.Models;
using Tareas.CtxTarea3;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tareas.Controllers
{
    public class Tarea4Controller : Controller
    {
        public IActionResult Index()
        {
            ViewData["Route"] = new List<HomeButtonsRequest>{
                new HomeButtonsRequest{Text="Home", ActionName = "Home"},
                new HomeButtonsRequest{Text = "Tarea 4", ActionName="Tarea4"}
            };
            return View();
        }
    }
}











