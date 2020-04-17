using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Core
{
    public class ValidateVotingProcess : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var error = string.Empty;
            using (var unityWork = new UnityOfWork(new Persistence.SVContext()))
            {
                var eleccion = unityWork.EleccionesRepository.Entity.LastOrDefault();
                if (eleccion != null && eleccion.EstadosId == (int)EstadosEleccionesEnum.EnVotacion)
                {
                    error = "Hay un proceso de votacion es estos momentos, no puede desactivar ni activar ciudadanos";
                    var controller = context.Controller.GetType().Name.Replace("Controller", string.Empty);
                    context.Result = new RedirectToActionResult("Index", controller, new { error });
                }

            }
        }
    }
}
