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

        public IActionResult IndexPokemon(int page = 1, int length = 10)
        {
            if(Request.Query["key"].ToString() != null)
                ModelState.AddModelError(Request.Query["key"].ToString(), Request.Query["error"].ToString());

            var paginador = new Paginator<Pokemon, PokemonListResponse>(this.context.Pokemon,page, length, nameof(this.IndexPokemon), "Tarea4");
            return View(paginador.Pagination);
        }

        public IActionResult NewPokemon()
        {
            if(this.context.Region.Count() == 0 || this.context.Tipo.Count() == 0)
                return RedirectToAction(nameof(this.IndexPokemon), new {key = "NotCreate", error = "No hay regiones o tipos creados, verifique"});
            
            var listRegiones = this.context.Region.ToList().Select(x=>(RegionesListResponse)x);
            ViewBag.Regiones = listRegiones;
            var listTipos = this.context.Tipo.ToList().Select(x=>(TipoListResponse)x);
            ViewBag.Tipos = listTipos;
            // ViewBag.Ataques;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewPokemon(PokemonRequest pokemon)
        {
            // this.context.Pokemon(new Pokemon{
            //     Id = pokemon.Id,
            //     Nombre = pokemon.Nombre,
            //     RegionId
            // });
            return RedirectToAction(nameof(this.IndexPokemon));
        }

        public IActionResult IndexRegion(int page = 1, int length = 10)
        {
            var model = new Paginator<Region, RegionesListResponse>(this.context.Region,page, length, nameof(this.IndexRegion), "Tarea4");
            return View(model.Pagination);
        }

        public IActionResult NewRegion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRegion(RegionesListResponse region)
        {
            await this.context.Region.AddAsync((Region)region);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexRegion));
        }

        public async Task<IActionResult> EditRegion(int id)
        {
            var entity = await this.context.Region.FirstOrDefaultAsync(x=>x.Id == id);
            var region = (RegionesListResponse)entity;
            return View(region);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRegion(RegionesListResponse region)
        {
            var entity = (Region)region;
            this.context.Region.Update(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexRegion));
        }

        public IActionResult IndexTipo(int page = 1, int length = 10)
        {
            var paginator = new Paginator<Tipo, TipoListResponse>(this.context.Tipo, page, length, nameof(this.IndexTipo), "Tarea4");
            return View(paginator.Pagination);
        }

        public IActionResult NewTipo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewTipo(TipoListResponse tipo)
        {
            var entity = (Tipo)tipo;
            this.context.Tipo.Update(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexTipo));
        }

        public async Task<IActionResult> EditTipo(int id)
        {
            var entity = await this.context.Tipo.FirstOrDefaultAsync(x=> x.Id == id);
            var tipo = (TipoListResponse)entity;
            return View(tipo);
        }
    }
}

