using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tareas.Models;
using Tareas.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Tareas.CtxTarea5;

namespace Tareas.Controllers
{
    [Authorize]
    public class Tarea5Controller : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly Tarea5Context context;

        public Tarea5Controller(
            UserManager<IdentityUser> UserManager,
            SignInManager<IdentityUser> SignInManager,
            Tarea5Context context
        )
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            this.context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if(
                ModelState.IsValid
                && this.SignInManager.IsSignedIn(this.User)
            )
            {
                var publicaciones = this.context.Publicacion.Where(x=>x.IdUser == this.User.Identity.Name).ToList();
                return View(publicaciones);
            }
            return View();
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(PublicacionDto publicacion)
        {
            await this.context.Publicacion.AddAsync(new Publicacion
            {
                Titulo = publicacion.Title,
                Contenido = publicacion.Contenido,
                IdUser = this.User.Identity.Name,
            });
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await this.context.Publicacion.FirstOrDefaultAsync(x=>x.Id == id);
            this.context.Publicacion.Remove(entity);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await this.context.Publicacion.FirstOrDefaultAsync(x=>x.Id == id);
            var publicacion = new PublicacionDto
            {
                Id = entity.Id,
                Title = entity.Titulo,
                Contenido = entity.Contenido,
            };
            return View(publicacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PublicacionDto publicacion)
        {
            var entity = new Publicacion
            {
                Id = publicacion.Id,
                Titulo = publicacion.Title,
                Contenido = publicacion.Contenido,
            };
            var entry = this.context.Entry(entity);
            entry.Property("Titulo").IsModified = true;
            entry.Property("Contenido").IsModified = true;
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(this.Index));
        }
    }
}