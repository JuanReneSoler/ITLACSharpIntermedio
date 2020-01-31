using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tarea1.Models;

namespace Tarea1.Controllers
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

        public IActionResult p1(){
            return PartialView("~/Views/Home/partials/p1.cshtml");
        }

        [HttpPost]
        public IActionResult p1(Models.p1Request p1){
            return PartialView("~/Views/Home/partials/p1.cshtml", p1);
        }

        public IActionResult p2(){
            return PartialView("~/Views/Home/partials/p2.cshtml");
        }

        [HttpPost]
        public IActionResult p2(Models.p2Request p2){
            return PartialView("~/Views/Home/partials/p2.cshtml");
        }

        public ActionResult p3(){
            return PartialView("~/Views/Home/partials/p3.cshtml");
        }
    }
}
