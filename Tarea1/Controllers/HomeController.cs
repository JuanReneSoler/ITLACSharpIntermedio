using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tarea1.Models;

namespace Tarea1.Controllers
{
    public class HomeController : Controller
    {


        private string Signos(DateTime fecha){
            var year = fecha.Year;
            var list = new List<ListaSignosModel>{
                new ListaSignosModel{Nombre="Aries",      FechaInicio=new DateTime(year,3,21), FechaFin=new DateTime(year,4,20)},
                new ListaSignosModel{Nombre="Tauro",      FechaInicio=new DateTime(year,4,21), FechaFin=new DateTime(year,4,20)},
                new ListaSignosModel{Nombre="Géminis", 	  FechaInicio=new DateTime(year,5,21), FechaFin=new DateTime(year,6,24)},
                new ListaSignosModel{Nombre="Cáncer", 	  FechaInicio=new DateTime(year,6,25), FechaFin=new DateTime(year,7,22)},
                new ListaSignosModel{Nombre="Leo", 	      FechaInicio=new DateTime(year,7,23), FechaFin=new DateTime(year,8,23)},
                new ListaSignosModel{Nombre="Virgo", 	  FechaInicio=new DateTime(year,8,24), FechaFin=new DateTime(year,9,23)},
                new ListaSignosModel{Nombre="Libra", 	  FechaInicio=new DateTime(year,9,24), FechaFin=new DateTime(year,10,22)},
                new ListaSignosModel{Nombre="Escorpio",   FechaInicio=new DateTime(year,10,23),FechaFin=new DateTime(year,11,22)},
                new ListaSignosModel{Nombre="Sagitario",  FechaInicio=new DateTime(year,11,22),FechaFin=new DateTime(year,12,21)},
                new ListaSignosModel{Nombre="Capricornio",FechaInicio=new DateTime(year,12,22),FechaFin=new DateTime(year,1,19)},
                new ListaSignosModel{Nombre="Acuario", 	  FechaInicio=new DateTime(year,1,20), FechaFin=new DateTime(year,2,18)},
                new ListaSignosModel{Nombre="Piscis", 	  FechaInicio=new DateTime(year,2,19), FechaFin=new DateTime(year,3,20)}
            };

            return list.FirstOrDefault(x=>x.FechaInicio<=fecha && x.FechaFin>=fecha).Nombre;
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
            if(!ModelState.IsValid)
                return PartialView("~/Views/Home/partials/p1.cshtml",p1);

            p1.resultado = $"Su signo sodiacal es: {this.Signos(p1.fechaNacimiento.Date)}";
            return PartialView("~/Views/Home/partials/p1.cshtml",p1);
        }

        public IActionResult p2(){
            return PartialView("~/Views/Home/partials/p2.cshtml");
        }

        [HttpPost]
        public IActionResult p2(Models.p2Request p2){
            var value = Math.Pow(p2.b,2)-4*p2.a*p2.c;
            var i = string.Empty;
            if(value < 0){
                value *= (-1);
                i = "i";
            }
            var d = Math.Pow(value, 0.5);
            var v1 = (-1*p2.b+d)/(2*p2.a);
            var v2 = (-1*p2.b-d)/(2*p2.a);
            p2.resultado = $"x1={v1}{i}, x2={v2}{i}";
            return PartialView("~/Views/Home/partials/p2.cshtml", p2);
        }

        public IActionResult p3()
        {
            return PartialView("~/Views/Home/partials/p3.cshtml");
        }
    }
}
