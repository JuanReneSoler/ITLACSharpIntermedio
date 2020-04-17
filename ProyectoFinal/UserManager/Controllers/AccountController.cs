using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Core;
using Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;

namespace UserManager.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> UserManager;
        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.UserManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult New()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(AdministradoresRequest administrador)
        {
            if (this.UserManager.Users.Any(x => x.UserName == administrador.UserName))
                ModelState.AddModelError("", "Este usuario ya existe.");
            if(ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = administrador.UserName,
                    Email = administrador.Email
                };
                var result = await this.UserManager.CreateAsync(user, administrador.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(this.Index));
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(administrador);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, int length = 3)
        {
            if (!this.UserManager.Users.Any())
                return RedirectToAction(nameof(this.New));
            
            var list = await this.UserManager.Users.ToListAsync();
            var model = list.Select(x=>
                new AdministradoresListResponse
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Estado = x.LockoutEnabled?"Activo":"Inactivo",
                    Email = x.Email
                });
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEstado(string id)
        {
            var user = await this.UserManager.Users.FirstOrDefaultAsync(x=>x.Id == id);
            user.LockoutEnabled = !user.LockoutEnabled;
            var result = await this.UserManager.UpdateAsync(user);
            return RedirectToAction(nameof(this.Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.UserManager.Users.FirstOrDefaultAsync(x=>x.Id == id);
            var model = new AdministradoresRequest
            {
                Email = user.Email,
                Estado = user.LockoutEnabled,
                Id = user.Id,
                UserName = user.UserName,
                SecurityStamp = user.SecurityStamp
            };
            return View(model);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(AdministradoresRequest administrador)
        {
            if(ModelState.IsValid)
            {
                var user = await this.UserManager.Users.FirstOrDefaultAsync(x=>x.Id == administrador.Id);
                user.UserName = administrador.UserName;
                user.Email = administrador.Email;
                user.LockoutEnabled = administrador.Estado;
                var has = this.UserManager.PasswordHasher;
                user.PasswordHash = has.HashPassword(user ,administrador.Password);
                var result = await this.UserManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(this.Index));
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(administrador);
        }


        // [HttpPost]
        // [AllowAnonymous]
        // public async Task<IActionResult> Detail(string id)
        // {
        //     var entity = await this.UserManager.Users.FirstOrDefaultAsync(x=>x.Id == id);
        //     var model = new AdministradoresRequest
        //     {
        //         Email = entity.Email,
        //         Estado = entity.LockoutEnabled,
        //         UserName = entity.UserName
        //     };
        //     return View(model);
        // }
    }
}