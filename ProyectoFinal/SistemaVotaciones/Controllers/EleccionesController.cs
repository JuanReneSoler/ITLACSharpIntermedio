using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Core;
using Microsoft.AspNetCore.Authorization;
using Tools;
using Rotativa.AspNetCore;
using System.Linq;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaVotaciones.Controllers
{

    public enum TipoGrafico
    {
        Tabla = 1,
        Barra = 2,
        Pie = 3
    }

    [Authorize]
    public class EleccionesController : Controller
    {
        readonly UnityOfWork unityWork;
        //readonly IReportGenerator reportGenerator;

        public EleccionesController(
            UnityOfWork unityWork)
        {
            this.unityWork = unityWork;
            //this.reportGenerator = new LocalReportGenerator();
        }
        public IActionResult Index(int page = 1, int length = 3)
        {
            if (Request.Query["error"].ToString() != string.Empty)
                ModelState.AddModelError("", Request.Query["error"]);
            var paged = new Paginator<Elecciones>(this.unityWork.EleccionesRepository.Select().Include(x => x.Estados), page, length).Pagination;
            ViewBag.Pagination = new Models.PaginationDto { Action = nameof(this.Index), Controller = "Elecciones", CurrentPage = page, Length = length, Pages = paged.Pages };
            return View(paged.ListaDatos.Select(x => (EleccionesListResponse)x));
        }

        [ValidateVotingProcess]
        [DeactivateByCreatedVote]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        [DeactivateByCreatedVote]
        public async Task<IActionResult> New(EleccionesRequest elecciones)
        {
            if (!ModelState.IsValid) return View(elecciones);

            var entity = (Elecciones)elecciones;
            entity.EstadosId = (int)EstadosEleccionesEnum.Creada;
            await this.unityWork.EleccionesRepository.Entity.AddAsync(entity);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        [ValidateVotingProcess]
        [DeactivateByClosedVote]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await this.unityWork.EleccionesRepository.Entity.FirstOrDefaultAsync(x => x.Id == id);
            return View((EleccionesRequest)entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateVotingProcess]
        [DeactivateByClosedVote]
        public async Task<IActionResult> Edit(EleccionesRequest elecciones)
        {
            if (!ModelState.IsValid) return View(elecciones);

            var entity = (Elecciones)elecciones;
            await this.unityWork.EleccionesRepository.Update(entity, x => x.Nombre, x => x.FechaRealizadion);
            await this.unityWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var entity = await this.unityWork.EleccionesRepository.Entity.FirstOrDefaultAsync(x => x.Id == id);
            return View((EleccionesRequest)entity);
        }

        //public async Task<IActionResult> ChangeEstado(int id)
        //{
        //    if(this.unityWork.CandidatosRepository.Select(x => x.EleccionId == id).Count() >= 2)
        //    {
        //        await this.unityWork.EleccionesRepository.ChangeEstado(id);
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction(nameof(this.Index), new { error = "" });

        //}

        [ValidateVotingProcess]
        [DeactivateByClosedVote]
        public async Task<IActionResult> IniciarVotaciones(int id)
        {
            if (this.unityWork.CandidatosRepository.Select(x => x.EleccionId == id && x.Estado == true).Count() >= 2)
            {
                await this.unityWork.EleccionesRepository.Update(new Elecciones { Id = id, EstadosId = (int)EstadosEleccionesEnum.EnVotacion }, x => x.EstadosId);
                await this.unityWork.Commit();
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(this.Index), new { error = "Deben existir minimo 2 candidatos creados y activos en el sistema para poder iniciar un prioceso electoral" });
        }

        [DeactivateByClosedVote]
        public async Task<IActionResult> CerrarElecciones(int id)
        {
            await this.unityWork.EleccionesRepository.Update(new Elecciones { Id = id, EstadosId = (int)EstadosEleccionesEnum.Cerrada }, x => x.EstadosId);
            await this.unityWork.Commit();
            return RedirectToAction("Index");
        }

        [ValidateVotingProcess]
        public async Task<IActionResult> Resultado(int id)
        {
            return new ViewAsPdf("Resultado", await this.unityWork.EleccionesRepository.ResultadoEleccion(id));
        }

        public IActionResult Graficos()
        {
            var result = this.unityWork.EleccionesRepository.Entity.Include(x=>x.Estados).ToList().Select(x=>(EleccionesListResponse)x);

            if (result == null || !result.Any())
                return RedirectToAction(nameof(this.Index), new { error = "No hay elecciones creadas." });

            return View(result);

        }

        public async Task<IActionResult> Puestos(int id)
        {
            var result = await this.unityWork.EleccionesRepository.ResultadoEleccion(id);
            return Json(result.Select(x=>x.PuestoName));
        }


        public async Task<IActionResult> ChartBarra(int id, string puesto)
        {

            var result = await this.unityWork.EleccionesRepository.ResultadoEleccion(id);

            var json = new
            {
                xAxis = result.Where(x=>x.PuestoName == puesto).Select(x=>x.PuestoName),
                //categories = result.FirstOrDefault().Candidatos.Select(x=>x.NombreCompleto).ToArray(),
                series = result.Where(x=>x.PuestoName == puesto).FirstOrDefault().Candidatos.GroupBy(x=>x.NombreCompleto, x=>x, (k,g)=> new{
                    name = k,
                    data = new int[] { g.Sum(x=>x.Votos) }
                
                })
            };

            return Json(json);
        }

        public async Task<IActionResult> ChartPie(int id, string puesto)
        {

            var result = await this.unityWork.EleccionesRepository.ResultadoEleccion(id);

            var json = new
            {
                xAxis = result.Where(x => x.PuestoName == puesto).Select(x => x.PuestoName),
                //categories = result.FirstOrDefault().Candidatos.Select(x=>x.NombreCompleto).ToArray(),
                series = result.Where(x => x.PuestoName == puesto).FirstOrDefault().Candidatos.Select(x=>new {
                    name = x.NombreCompleto,
                    y = x.Votos
                }).ToArray()
            };

            return Json(json);
        }
    }
}