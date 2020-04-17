using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core;
using Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tools;

namespace SistemaVotaciones.Controllers
{
    [Authorize]
    public class CiudadanoController : Controller
    {
        private UnityOfWork unityWork;

        public CiudadanoController(UnityOfWork unityWork)
        {
            this.unityWork = unityWork;
        }

        public IActionResult Index(int page = 1, int length = 3)
        {
            if (Request.Query["error"].ToString() != string.Empty)
                ModelState.AddModelError("", Request.Query["error"]);

            var result = new Paginator<Ciudadanos>(this.unityWork.CiudadanoRepository.Select(), page, length).Pagination;
            ViewBag.Pagination = new Models.PaginationDto { Pages = result.Pages, Controller = "Ciudadano", Action = nameof(this.Index), CurrentPage = page, Length = length };
            return View(result.ListaDatos.Select(x => (CiudadanoListResponse)x));
        }


        [ValidateVotingProcess]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        public async Task<IActionResult> New(CiudadanoRequest ciudadano)
        {
            if (!ModelState.IsValid) return View(ciudadano);

            var entity = (Ciudadanos)ciudadano;
            entity.Estado = true;
            await this.unityWork.CiudadanoRepository.Entity.AddAsync(entity);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var entity = await this.unityWork.CiudadanoRepository.Entity.FirstOrDefaultAsync(x => x.Id == id);
            return View((CiudadanoRequest)entity);
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    await this.repository.Remove(id);
        //    return RedirectToAction(nameof(Index));
        //}

        [ValidateVotingProcess]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await this.unityWork.CiudadanoRepository.Entity.FirstOrDefaultAsync(x => x.Id == id);
            return View((CiudadanoRequest)entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        public async Task<IActionResult> Edit(CiudadanoRequest ciudadano)
        {
            if (!ModelState.IsValid) return View(ciudadano);

            var entity = (Ciudadanos)ciudadano;
            await this.unityWork.CiudadanoRepository.Update(entity, x => x.DocIdentidad, x => x.Nombre, x => x.Apellido, x => x.Email);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        [ValidateVotingProcess]
        public async Task<IActionResult> ChangeEstado(int id)
        {
            await this.unityWork.CiudadanoRepository.ChangeEstado(id);
            await this.unityWork.Commit();
            return RedirectToAction("Index");
        }
    }
}
