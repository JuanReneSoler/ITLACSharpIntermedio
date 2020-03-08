using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tareas.Models;
using Tareas.CtxTarea4;
using Tareas.Tools;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Tareas.Controllers
{
    public class Tarea4Controller : Controller
    {
        private Tarea4Context context;
        public Tarea4Controller(Tarea4Context ctx)
        {
            this.context = ctx;
        }

        public IActionResult Index(int page = 1, int length = 10)
        {
            ViewData["Route"] = new List<HomeButtonsRequest>{
                new HomeButtonsRequest{Text="Home", ActionName = "Home"},
                new HomeButtonsRequest{Text = "Tarea 4", ActionName="Tarea4"}
            };
            if(Request.Query["key"].ToString() != null)
                ModelState.AddModelError(Request.Query["key"].ToString(), Request.Query["error"].ToString());

            var paginador = new Paginator<Pokemon, PokemonListResponse>(this.context.Pokemon,page, length, nameof(this.Index), "Tarea4");
            return View(paginador.Pagination);
        }

        public IActionResult New()
        {
            if(this.context.Region.Count() == 0 || this.context.Tipo.Count() == 0)
                return RedirectToAction(nameof(this.Index), new {key = "NotCreate", error = "No hay regiones o tipos creados, verifique"});
            // ViewBag.Regiones;
            // ViewBag.Tipos;
            // ViewBag.Ataques;
            return View();
        }

        public IActionResult RegionesIndex(int page = 1, int length = 10)
        {
            var model = new Paginator<Region, RegionesListResponse>(this.context.Region,page, length, nameof(this.RegionesIndex), "Tarea4");
            return View(model.Pagination);
        }

        public IActionResult NewRegion()
        {
            return View();
        }

        public async Task<IActionResult> SaveRegion(RegionesListResponse region)
        {
            await this.context.Region.AddAsync((Region)region);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.RegionesIndex));
        }

        public IActionResult TiposIndex(int page = 1, int length = 10)
        {
            var paginator = new Paginator<Tipo, TipoListResponse>(this.context.Tipo, page, length, nameof(this.TiposIndex), "Tarea4");
            return View(paginator.Pagination);
        }
    }
}

