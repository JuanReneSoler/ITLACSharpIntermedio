using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Core;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SistemaVotaciones.Controllers
{
    public enum VotacionKeys
    {
        Votante,
        VotanteName,
        VotanteEmail,
        Eleccion,
        EleccionName,
        Candidatos
    }
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UnityOfWork unityWork;

        public AccountController(
            UserManager<IdentityUser> UserManager,
            SignInManager<IdentityUser> SigInManager,
            UnityOfWork unityWork
        )
        {
            this.UserManager = UserManager;
            this.SignInManager = SigInManager;
            this.unityWork = unityWork;
        }

        [AllowAnonymous]
        public IActionResult LogIn()
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("StartPage", "Home");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogInVotantes(string cedula)
        {
            HttpContext.Session.Clear();

            if(string.IsNullOrEmpty(cedula) || cedula.Length != 13)
            {
                ModelState.AddModelError("", "Debe suministrar un cedula valida");
                return View("~/Views/Home/Index.cshtml");
            }
            cedula = cedula.Replace("-", string.Empty);
            var ciudadano = await this.unityWork.CiudadanoRepository.Entity.FirstOrDefaultAsync(x => x.DocIdentidad == cedula && x.Estado == true);
            if (ciudadano == null)
            {
                ModelState.AddModelError("", "La informacion suministrada no es valida, esto se puede deber a que su informacion no esta registrada en el sistema o no esta habilitado para estas votaciones, favor comunicarse con el administrador.");
                return View("~/Views/Home/Index.cshtml");
            }

            var eleccion = await this.unityWork.EleccionesRepository.Entity.LastOrDefaultAsync(x => x.EstadosId == (int)EstadosEleccionesEnum.EnVotacion);

            if (
                eleccion == null
                )
            {
                ModelState.AddModelError("", "No hay ningun proceso elecctoral en estos momentos.");
                return View("~/Views/Home/Index.cshtml");
            }

            if (
                await this.unityWork.VotacionRepository.Entity.AnyAsync(x => x.CiudadanoId == ciudadano.Id && x.EleccionId == eleccion.Id)
                )
            {
                ModelState.AddModelError("", "Usted ya ha ejercido su derecho al voto, vuelva para las elecciones siguientes, gracias.");
                return View("~/Views/Home/Index.cshtml");
            }

            HttpContext.Session.SetInt32(VotacionKeys.Votante.ToString(), ciudadano.Id);
            HttpContext.Session.SetString(VotacionKeys.VotanteName.ToString(), ciudadano.Nombre);
            HttpContext.Session.SetString(VotacionKeys.VotanteEmail.ToString(), ciudadano.Email);
            HttpContext.Session.SetInt32(VotacionKeys.Eleccion.ToString(), eleccion.Id);
            HttpContext.Session.SetString(VotacionKeys.EleccionName.ToString(), eleccion.Nombre);
            HttpContext.Session.SetString(VotacionKeys.Candidatos.ToString(), JsonConvert.SerializeObject(new List<Models.SeleccionModel>()));
            return RedirectToAction("Index", "Votacion");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogOutVotantes()
        {
            var ciudadano = HttpContext.Session.GetString(VotacionKeys.Votante.ToString());

            var message = string.Empty;

            if (!string.IsNullOrEmpty(ciudadano) && !string.IsNullOrWhiteSpace(ciudadano))
                message = "Listo! su votacion se realizo con exito, verifique su correo.";

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home", new { message });
        }

        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInRequest logIn)
        {
            if(ModelState.IsValid)
            {
                var user = await this.UserManager.FindByNameAsync(logIn.UserName);
                var result = await this.UserManager.CheckPasswordAsync(user, logIn.Password);
                if (result)
                {
                    await this.SignInManager.SignInAsync(user, logIn.RememberMe, null);
                    return RedirectToAction("StartPage", "Home");
                }
            }
            return View(logIn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await this.SignInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}