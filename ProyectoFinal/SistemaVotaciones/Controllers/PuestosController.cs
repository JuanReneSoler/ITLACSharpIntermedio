

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVotaciones.Models;
using Models.Dtos;
using Core;
using Microsoft.AspNetCore.Authorization;
using Models.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace SistemaVotaciones.Controllers
{
    [Authorize]
    public class PuestosController : Controller
    {
        readonly UnityOfWork unityWork;
        public PuestosController(UnityOfWork unityWork)
        {
            this.unityWork = unityWork;
        }
        public IActionResult Index(int page = 1, int length = 3)
        {
            if (Request.Query["error"].ToString() != string.Empty)
                ModelState.AddModelError("", Request.Query["error"]);

            var paged = new Paginator<PuestosElectivos>(this.unityWork.PuestosRepository.Select(), page, length).Pagination;
            ViewBag.Pagination = new PaginationDto { Action = nameof(this.Index), CurrentPage = page, Controller = "Puestos", Length = length, Pages = paged.Pages };
            return View(paged.ListaDatos.Select(x => (PuestosListResponse)x));
        }

        [ValidateVotingProcess]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        public async Task<IActionResult> New(PuestoRequest puesto)
        {
            if (!ModelState.IsValid) return View(puesto);

            var entity = (PuestosElectivos)puesto;
            entity.Estado = true;
            await this.unityWork.PuestosRepository.Entity.AddAsync(entity);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        [ValidateVotingProcess]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await this.unityWork.PuestosRepository.Entity.FirstOrDefaultAsync(x => x.Id == id);
            return View((PuestoRequest)entity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        public async Task<IActionResult> Edit(PuestoRequest puesto)
        {
            if (!ModelState.IsValid) return View(puesto);

            var entity = (PuestosElectivos)puesto;
            await this.unityWork.PuestosRepository.Update(entity, x=>x.Nombre, x=>x.Descripcion);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        [ValidateVotingProcess]
        public async Task<IActionResult> ChangeEstado(int id)
        {
            await this.unityWork.PuestosRepository.ChangeEstado(id);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var entity = await this.unityWork.PuestosRepository.Entity.FirstOrDefaultAsync(x=>x.Id == id);
            return View((PuestoRequest)entity);
        }
    }
}