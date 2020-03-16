using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tareas.Models;
using Tareas.CtxTarea4;
using Tareas.Tools;
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

            var list = this.context.Pokemon
                .Include(x=> x.RegionPokemon).ThenInclude(x=>x.Region)
                .Include(x=>x.TipoPokemon).ThenInclude(x=>x.Tipo)
                .Include(x=>x.Ataques);

            var paginador = new Paginator<Pokemon, PokemonListResponse>(list,page, length, nameof(this.IndexPokemon), "Tarea4");
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
            var entity = (Pokemon)pokemon;
            await this.context.Pokemon.AddAsync(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexPokemon));
        }

        public async Task<IActionResult> EditPokemon(int id)
        {
            var entity = await this.context.Pokemon
                        .Include(x=>x.RegionPokemon).ThenInclude(x=>x.Region)
                        .Include(x=>x.TipoPokemon).ThenInclude(x=>x.Tipo)
                        .Include(x=>x.Ataques)
                        .FirstOrDefaultAsync(x=> x.Id == id);

            var listRegiones = this.context.Region.ToList().Select(x=>(RegionesListResponse)x);
            ViewBag.Regiones = listRegiones;
            var listTipos = this.context.Tipo.ToList().Select(x=>(TipoListResponse)x);
            ViewBag.Tipos = listTipos;
            return View((PokemonRequest)entity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPokemon(PokemonRequest pokemon)
        {
            var entity = (Pokemon)pokemon;
            this.context.Pokemon.Update(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexPokemon));
        }

        public async Task<IActionResult> DeletePokemon(int id)
        {
            var entity = await this.context.Pokemon.FirstOrDefaultAsync(x=>x.Id == id);
            var regiones = this.context.RegionPokemon.Where(x=>x.PokemonId == id);
            var tipos = this.context.TipoPokemon.Where(x=>x.PokemonId == id);
            var atakes = this.context.Ataques.Where(x=>x.PokemonId == id);
            this.context.Ataques.RemoveRange(atakes);
            this.context.TipoPokemon.RemoveRange(tipos);
            this.context.RegionPokemon.RemoveRange(regiones);
            this.context.Pokemon.Remove(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexPokemon));
        }

        public async Task<IActionResult> DetailPokemon(int id)
        {
            var entity = await this.context.Pokemon
                        .Include(x=>x.RegionPokemon).ThenInclude(x=>x.Region)
                        .Include(x=>x.TipoPokemon).ThenInclude(x=>x.Tipo)
                        .Include(x=>x.Ataques)
                        .FirstOrDefaultAsync(x=> x.Id == id);
            
            return View((PokemonRequest)entity);
        }

        public IActionResult IndexRegion(int page = 1, int length = 10)
        {
            if(Request.Query["key"].ToString() != null)
                ModelState.AddModelError(Request.Query["key"].ToString(), Request.Query["error"].ToString());

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
            var entity = (Region)region;
            await this.context.Region.AddAsync(entity);
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

        public async Task<IActionResult> DeleteRegion(int id)
        {
            if(this.context.RegionPokemon.Any(x=>x.RegionId == id))
                return RedirectToAction(nameof(this.IndexRegion), new {key = "NotCreate", error = "La region no puede ser borrada, por que algun pokemon la usa."});

            var entity = await this.context.Region.FirstOrDefaultAsync(x=>x.Id == id);
            if(this.context.RegionPokemon.Any(x=>x.RegionId == id))
            {
                var regionpokemon = await this.context.RegionPokemon.FirstOrDefaultAsync(x=>x.RegionId == id);
                this.context.RegionPokemon.Remove(regionpokemon);
            }
            this.context.Region.Remove(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexRegion));
        }

        public async Task<IActionResult> DetailRegion(int id)
        {
            var entity = await this.context.Region.FirstOrDefaultAsync(x=>x.Id == id);
            var region = (RegionesListResponse)entity;
            return View(region);
        }

        public IActionResult IndexTipo(int page = 1, int length = 10)
        {
            if(Request.Query["key"].ToString() != null)
                ModelState.AddModelError(Request.Query["key"].ToString(), Request.Query["error"].ToString());

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
            await this.context.Tipo.AddAsync(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexTipo));
        }

        public async Task<IActionResult> EditTipo(int id)
        {
            var entity = await this.context.Tipo.FirstOrDefaultAsync(x=> x.Id == id);
            var tipo = (TipoListResponse)entity;
            return View(tipo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTipo(TipoListResponse tipo)
        {
            var entity = (Tipo)tipo;
            this.context.Tipo.Update(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexTipo));
        }

        public async Task<IActionResult> DeleteTipo(int id)
        {
            if(this.context.TipoPokemon.Any(x=>x.TipoId == id))
                return RedirectToAction(nameof(this.IndexTipo), new {key = "NotCreate", error = "El tipo no puede ser borrado, porque algun pokemon la usa."});
            
            var entity = await this.context.Tipo.FirstOrDefaultAsync(x=>x.Id == id);
            if(this.context.TipoPokemon.Any(x=> x.TipoId == id))
            {
                var tipopokemon = await this.context.TipoPokemon.FirstOrDefaultAsync(x=>x.TipoId == id);
                this.context.TipoPokemon.Remove(tipopokemon);
            }
            this.context.Tipo.Remove(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.IndexTipo));
        }

        public async Task<IActionResult> DetailTipo(int id)
        {
            var entity = await this.context.Tipo.FirstOrDefaultAsync(x=> x.Id == id);
            var tipo = (TipoListResponse)entity;
            return View(tipo);
        }
    }
}

