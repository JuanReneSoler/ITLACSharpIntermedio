using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace SistemaVotaciones.Controllers
{
    [Authorize]
    public class CandidatosController : Controller
    {
        private readonly UnityOfWork unityWork;

        public CandidatosController(
            UnityOfWork unityWork)
        {
            this.unityWork = unityWork;
        }

        public async Task<IActionResult> Index(int page=1,int length =3)
        {
            if (Request.Query["error"].ToString() != string.Empty)
                ModelState.AddModelError("", Request.Query["error"].ToString());

            var eleccion = await this.unityWork.EleccionesRepository.Entity.LastOrDefaultAsync();

            if (eleccion == null)
                return RedirectToAction(nameof(this.Index), "Elecciones", new { error = "No hay ninguna eleccion creada. debe verificar y itentar de nuevo" });

            ViewData["Title"] = eleccion.Nombre;

            ViewData["LastActivatedId"] = eleccion.Id;

            var list = this.unityWork.CandidatosRepository.GetAllByEleccion(eleccion.Id);

            var paginator = new Paginator<Candidatos>(list, page, length).Pagination;

            ViewBag.Pagination = new Models.PaginationDto { Action = nameof(this.Index), Controller = "Candidatos", CurrentPage = page, Length = length, Pages = paginator.Pages };

            return View(paginator.ListaDatos.Select(x => (CandidatosListResponse)x));
        }

        [ValidateVotingProcess]
        [DeactivateByClosedVote]
        public async Task<IActionResult> New(int eleccionId)
        {
            var eleccion = await this.unityWork.EleccionesRepository.Entity.FirstOrDefaultAsync(x=>x.Id == eleccionId);

            if(eleccion == null)
                return RedirectToAction(nameof(this.Index), new { error = "error al intentar abrir esta accion, no existe eleccion activa en estos momentos" });

            ViewData["LastActivatedId"] = eleccionId;

            ViewData["LastActividadText"] = eleccion.Nombre;

            var ciudadanosList = await this.unityWork.CiudadanoRepository
                .Select(x=>x.Estado == true 
                    && !this.unityWork.CandidatosRepository.Entity.Any(y=>y.EleccionId == eleccionId && y.CiudadanoId == x.Id)).ToListAsync();

            var listPartidos = await this.unityWork.PartidosRepository.Select(x=>x.Estado == true).ToListAsync();

            var listPuestos = await this.unityWork.PuestosRepository.Select(x=>x.Estado == true).ToListAsync();

            //if (listPuestos.Count < 4)
            //    return RedirectToAction(nameof(this.Index), new { error = "Debes haber minimo 4 puestos activos en el sistema." });

            if (
                !ciudadanosList.Any()
                || !listPartidos.Any()
                || !listPuestos.Any()
                )
                return RedirectToAction(nameof(this.Index), new { error = "error al intentar abrir esta accion, revise si ha definido ciudadanos, partidos y puestos previemente." });

            ViewBag.Ciudadanos = new SelectList(ciudadanosList.Select(x=>(CiudadanoListResponse)x), "Id", "NombreCompleto");
            
            ViewBag.Partidos = new SelectList(listPartidos.Select(x=>(PartidosListResponse)x),"Id","Nombre");

            ViewBag.Puestos = new SelectList(listPuestos.Select(x=>(PuestosListResponse)x), "Id", "Nombre");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        [DeactivateByClosedVote]
        public async Task<IActionResult> New(CandidatosRequest candidato)
        {
            if (!ModelState.IsValid) return View(candidato);

            var entity = (Candidatos)candidato;
            entity.Estado = true;
            await this.unityWork.CandidatosRepository.Entity.AddAsync(entity);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        [ValidateVotingProcess]
        [DeactivateByClosedVote]
        public async Task<IActionResult> Edit(int id, int eleccionId)
        {
            var eleccion = await this.unityWork.EleccionesRepository.Entity.FirstOrDefaultAsync(x=>x.Id == eleccionId);

            if (eleccion == null)
                return RedirectToAction(nameof(this.Index), new { error = "error al intentar abrir esta accion, no existe eleccion activa en estos momentos" });

            ViewData["LastActivatedId"] = eleccionId;
            
            ViewData["LastActividadText"] = eleccion.Nombre;
            
            var ciudadanosList = await this.unityWork.CiudadanoRepository
                .Select(x=>x.Estado == true 
                    && !this.unityWork.CandidatosRepository.Entity.Any(y=>y.EleccionId == eleccionId && y.CiudadanoId == x.Id)).ToListAsync();
            var listPartidos = await this.unityWork.PartidosRepository.Select(x=>x.Estado == true).ToListAsync();
            var listPuestos = await this.unityWork.PuestosRepository.Select(x=>x.Estado == true).ToListAsync();

            //if (listPuestos.Count() < 4)
            //    return RedirectToAction(nameof(this.Index), new { error = "Debe existir minimo 4 puestos activos en el sistema." });

            if (
                !ciudadanosList.Any()
                || !listPartidos.Any()
                || !listPuestos.Any()
                )
                return RedirectToAction(nameof(this.Index), new { error = "error al intentar abrir esta accion, revise si ha definido ciudadanos, partidos y puestos previemente." });

            ViewBag.Puestos = new SelectList(listPuestos.Select(x=>(PuestosListResponse)x), "Id", "Nombre");
            ViewBag.Ciudadanos = new SelectList(ciudadanosList.Select(x=>(CiudadanoListResponse)x), "Id", "NombreCompleto");
            ViewBag.Partidos = new SelectList(listPartidos.Select(x=>(PartidosListResponse)x), "Id", "Nombre");

            var entity = await this.unityWork.CandidatosRepository.Entity
                .Include(x=>x.Ciudadano)
                .Include(x=>x.Partido)
                .Include(x=>x.Puesto)
                .FirstOrDefaultAsync(x=>x.Id == id);

            return View((CandidatosRequest)entity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        [DeactivateByClosedVote]
        public async Task<IActionResult> Edit(CandidatosRequest candidatos)
        {
            if (!ModelState.IsValid) return View(candidatos);

            var entity = (Candidatos)candidatos;
            await this.unityWork.CandidatosRepository.Update(entity, x=>x.CiudadanoId, x=>x.PartidoId, x=>x.PuestoId, x=>x.FotoPerfil);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var entity = await this.unityWork.CandidatosRepository.Entity
                .Include(x=>x.Partido)
                .Include(x=>x.Puesto)
                .Include(x=>x.Ciudadano)
                .FirstOrDefaultAsync(x => x.Id == id);
            return View((CandidatosRequest)entity);
        }

        [ValidateVotingProcess]
        [DeactivateByClosedVote]
        public async Task<IActionResult> ChangeEstado(int id)
        {
            await this.unityWork.CandidatosRepository.ChangeEstado(id);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}