using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVotaciones.Models;
using Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Core;
using Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVotaciones.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //readonly UnityWork uw;

        // public HomeController(UnityWork uw)
        // {
        //     this.uw = uw;
        // }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (Request.Query["message"].ToString() != string.Empty)
                ModelState.AddModelError("", Request.Query["message"]);

            if(this.User.Identity.IsAuthenticated)
                return RedirectToAction("StartPage","Home");
            return View();
        }

        public IActionResult StartPage()
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
    }
}
