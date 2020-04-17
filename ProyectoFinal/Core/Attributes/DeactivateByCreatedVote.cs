using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Core
{
    public class DeactivateByCreatedVote: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var error = string.Empty;
            using (var unityWork = new UnityOfWork(new Persistence.SVContext()))
            {
                var eleccion = unityWork.EleccionesRepository.Entity.LastOrDefault();
                if (eleccion != null && eleccion.EstadosId == (int)EstadosEleccionesEnum.Creada)
                {
                    error = "Hay una votacion ya creada, solo se permite crear votaciones nuevas si no hay ninguna activa.";
                    var controller = context.Controller.GetType().Name.Replace("Controller", string.Empty);
                    context.Result = new RedirectToActionResult("Index", controller, new { error });
                }

            }
        }
    }
}
