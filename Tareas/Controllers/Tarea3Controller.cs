using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tareas.Models;
using Tareas.CtxTarea3;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Tareas.Controllers
{
    public class Carreras
    {
        public int id { get; set; }
        public string descripcion { get; set; }
    }
    [AllowAnonymous]
    public class Tarea3Controller:Controller
    {
        private Tarea3Context context;
        List<Carreras> list = new List<Carreras>{
                new Carreras {id=1, descripcion="Tecnologo de Software"},
                new Carreras {id=2, descripcion="Tecnologo de Mecatronica"},
                new Carreras {id=3, descripcion="Tecnolo de Manofactura utomatizada"},
                new Carreras {id=4 , descripcion="Tecnologo de Multimedia"}
            };
        public Tarea3Controller(Tarea3Context ctx)
        {
            this.context = ctx;
        }
        public IActionResult Index()
        {
            var list = context.Estudiantes.ToList().Select(x=> new EstudianteModel{
                id = x.Id,
                nombre = x.Nombre,
                apellido = x.Apellido,
                carreraText = this.list.FirstOrDefault(y=>y.id == x.CarreraId).descripcion,
                estado = x.Estado
            });
            return View(list);
        }

        public IActionResult New()
        {
            ViewData["MetodoSave"] = "Save";
            ViewBag.Carreras = new SelectList(this.list, "id", "descripcion");
            return View("EstudianteEdit",new EstudianteModel{estado = true});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(EstudianteModel estudiante)
        {
            if(!ModelState.IsValid || estudiante.carreraId == 0)
            {
                ViewData["MetodoSave"] = "Save";
                ViewBag.Carreras = new SelectList(this.list, "id", "descripcion");
                return View("EstudianteEdit");
            }

            this.context.Estudiantes.Add(new Estudiantes{
                Id = estudiante.id,
                Nombre = estudiante.nombre,
                Apellido = estudiante.apellido,
                Estado = estudiante.estado,
                CarreraId = estudiante.carreraId
            });
            this.context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var entity = this.context.Estudiantes.FirstOrDefault(x=>x.Id == id);
            this.context.Estudiantes.Remove(entity);
            this.context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewData["MetodoSave"] = "Update";
            ViewBag.Carreras = new SelectList(this.list, "id", "descripcion");
            var entity = this.context.Estudiantes.Where(x=>x.Id == id)
            .Select(x=>new EstudianteModel{
                id = x.Id,
                nombre = x.Nombre,
                apellido = x.Apellido,
                carreraId = x.CarreraId,
                estado = x.Estado
            }).FirstOrDefault();
            return View("EstudianteEdit", entity);
        }

        public IActionResult Update(EstudianteModel estudiante)
        {
            this.context.Estudiantes.Update(new Estudiantes{
                Id = estudiante.id,
                Nombre = estudiante.nombre,
                Apellido = estudiante.apellido,
                CarreraId = estudiante.carreraId,
                Estado = estudiante.estado
            });
            this.context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}