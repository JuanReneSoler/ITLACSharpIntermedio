using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Entities;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Tools;


namespace SistemaVotaciones.Controllers
{
    public class VotacionController : Controller
    {
        private readonly UnityOfWork unityWork;
        private readonly IMailSender mailSender;


        public VotacionController(
            UnityOfWork unityWork,
            GmailSender gmailSender)
        {
            this.unityWork = unityWork;
            this.mailSender = gmailSender;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("StartPage", "Home");

            if (Request.Query["error"].ToString() != string.Empty)
                ModelState.AddModelError("", Request.Query["error"]);

            var eleccion = HttpContext.Session.GetInt32(VotacionKeys.Eleccion.ToString()).Value;
            var list = await this.unityWork.PuestosRepository.GetByEleccionId(eleccion).ToListAsync();
            return View(list.Select(x=>(PuestosListResponse)x));
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Seleccion(int puesto, string puestoName)
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("StartPage", "Home");

            var seleccion = HttpContext.Session.GetString(VotacionKeys.Candidatos.ToString());
            var listSeleccion = JsonConvert.DeserializeObject<List<Models.SeleccionModel>>(seleccion);
            if (listSeleccion.Any(x => x.Puesto == puestoName))
                return RedirectToAction(nameof(this.Index), new { error = "Esta seleccion ya fue hecha, continue con la siguiente." });

            var eleccion = HttpContext.Session.GetInt32(VotacionKeys.Eleccion.ToString()).Value;
            var list = await this.unityWork.CandidatosRepository.GetAllByEleccionAndPuesto(eleccion, puesto).ToListAsync();
            ViewBag.Puesto = puestoName;
            list.Add(new Candidatos {
                Id = 0,
                Ciudadano = new Ciudadanos { Nombre = "Ninguno", Apellido = "", Estado = true },
                Puesto = new PuestosElectivos { Nombre = "", Id = 0 },
                Partido = new Partidos { Nombre = "", Id = 0, Estado = true },
                Estado = true,
                Votos = new List<Votos>()
            });
            return View(list.Select(x=>(CandidatosListResponse)x));
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCandidato(int id, string candidato, string partido, string puestoName)
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("StartPage", "Home");

            var json = HttpContext.Session.GetString(VotacionKeys.Candidatos.ToString());
            var list = JsonConvert.DeserializeObject<List<Models.SeleccionModel>>(json);
            if (!list.Exists(x => x.Puesto == puestoName))
            {
                list.Add(new Models.SeleccionModel
                {
                    Id = id,
                    Candidato = candidato,
                    Partido = partido,
                    Puesto = puestoName
                });
                json = JsonConvert.SerializeObject(list);
                HttpContext.Session.SetString(VotacionKeys.Candidatos.ToString(), json);
            }
            return RedirectToAction(nameof(this.Index));
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveVotacion()
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("StartPage", "Home");

            var json = HttpContext.Session.GetString(VotacionKeys.Candidatos.ToString());

            var list = JsonConvert.DeserializeObject<List<Models.SeleccionModel>>(json);

            var eleccion = HttpContext.Session.GetInt32(VotacionKeys.Eleccion.ToString()).Value;

            if (list.Count() != this.unityWork.PuestosRepository.GetByEleccionId(eleccion).Count())
                return RedirectToAction(nameof(Index), new { error = "Debe completar su seleccion por cada puesto" });

            var ciudadano = HttpContext.Session.GetInt32(VotacionKeys.Votante.ToString()).Value;

            await this.unityWork.VotacionRepository.Add(new VotacionRequest
            {
                CiudadanoId = ciudadano,
                EleccionId = eleccion,
                CandidatosId = list.Select(x => x.Id).ToArray()
            });

            await this.unityWork.Commit();

            Func<Task<string>> func = async () =>
                  {
                      var ciudadanoName = HttpContext.Session.GetString(VotacionKeys.VotanteName.ToString());
                      var votacionName = HttpContext.Session.GetString(VotacionKeys.EleccionName.ToString());
                      return await Task.Run(() =>
                      {
                          var html = @"
<h2>Listo!</h2>
Estimado <b>"+ciudadanoName+@"</b>:
<br/>
Usted acaba de ejercer su derecho al voto en "+votacionName+@"
<br/>
su seleccion fue la siguiente:

<ul><input type='hidden' /></ul>

<h3>Por favor No contestar este mensaje!</h3>
";
                          var result = string.Empty;
                          foreach (var item in list)
                          {
                              html = html.Replace("<input type='hidden' />", $"<li>{item.Candidato} ({item.Partido}) => {item.Puesto}</li>{"<input type='hidden' />"}");
                          }
                          return html;
                      });
                  };

            this.mailSender.Send(new string[] { HttpContext.Session.GetString(VotacionKeys.VotanteEmail.ToString()) }, "Confirmacion de Votacion", await func());
            
            return RedirectToAction("LogOutVotantes", "Account");
        }
    }
}