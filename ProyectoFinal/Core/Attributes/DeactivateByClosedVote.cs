using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Core
{
    public class DeactivateByClosedVote : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var error = string.Empty;
            using (var unityWork = new UnityOfWork(new Persistence.SVContext()))
            {
                var eleccion = unityWork.EleccionesRepository.Entity.LastOrDefault();
                if (eleccion != null && eleccion.EstadosId == (int)EstadosEleccionesEnum.Cerrada)
                {
                    error = "Esta eleccion esta cerrada debe habilitar una votacion nueva para poder realizar esta accion.";
                    var controller = context.Controller.GetType().Name.Replace("Controller", string.Empty);
                    context.Result = new RedirectToActionResult("Index", controller, new { error });
                }

            }
        }
    }
}
