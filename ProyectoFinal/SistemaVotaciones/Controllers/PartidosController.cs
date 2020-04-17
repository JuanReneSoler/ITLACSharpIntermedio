// using System.Security.Cryptography.X509Certificates;
// using System.Runtime.CompilerServices;
// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVotaciones.Models;
using Models.Dtos;
using Core;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace SistemaVotaciones.Controllers
{
    [Authorize]
    public class PartidosController : Controller
    {
        private UnityOfWork unityWork;

        public PartidosController(UnityOfWork unityWork)
        {
            this.unityWork = unityWork;
        }

        public IActionResult Index(int page = 1, int length = 3)
        {
            if (Request.Query["error"].ToString() != string.Empty)
                ModelState.AddModelError("", Request.Query["error"]);

            var paged = new Paginator<Partidos>(this.unityWork.PartidosRepository.Select(), page, length).Pagination;
            ViewBag.Pagination = new PaginationDto { Action = nameof(this.Index), Controller = "Partidos", CurrentPage = page, Length = length, Pages = paged.Pages };
            return View(paged.ListaDatos.Select(x => (PartidosListResponse)x));
        }

        [ValidateVotingProcess]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        public async Task<IActionResult> New(PartidoRequest partido)
        {
            if (!ModelState.IsValid) return View(partido);

            var entity = (Partidos)partido;
            entity.Estado = true;
            await this.unityWork.PartidosRepository.Entity.AddAsync(entity);
            await this.unityWork.Commit();
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    await this.repository.Remove(id);
        //    return RedirectToAction(nameof(Index));
        //}

        [ValidateVotingProcess]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await this.unityWork.PartidosRepository.Entity.FirstOrDefaultAsync(x => x.Id == id);
            return View((PartidoRequest)entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        public async Task<IActionResult> Edit(PartidoRequest partido)
        {
            if (!ModelState.IsValid) return View(partido);

            var entity = (Partidos)partido;
            await this.unityWork.PartidosRepository.Update(entity, x => x.Nombre, x => x.Descripcion, x => x.LogoPartido);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var entity = await this.unityWork.PartidosRepository.Entity.FirstOrDefaultAsync(x => x.Id == id);
            return View((PartidoRequest)entity);
        }

        [ValidateVotingProcess]
        public async Task<IActionResult> ChangeEstado(int id)
        {
            await this.unityWork.PartidosRepository.ChangeEstado(id);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}