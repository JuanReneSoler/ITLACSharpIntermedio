using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tareas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Tareas.CtxTarea5;

namespace Tareas.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly Tarea5Context context;
        public AccountController(
            UserManager<IdentityUser> UserManager,
            SignInManager<IdentityUser> SigInManager,
            Tarea5Context context)
        {
            this.UserManager = UserManager;
            this.SignInManager = SigInManager;
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto registro)
        {
            if(this.context.Users.Any(x=>x.UserName == registro.User))
                ModelState.AddModelError("","El Usuario ya existe");
                
            if(ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registro.User,
                    Email = registro.Email
                };
                var result = await UserManager.CreateAsync(user,registro.Password);
                if(result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user,false);
                    return RedirectToAction("Index", "Tarea5");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            
            return View(registro);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInDto user)
        {
            if(ModelState.IsValid)
            {
                await this.SignInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe,false);
                return RedirectToAction("Index", "Tarea5");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await this.SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Tarea5");
        }
    }
}