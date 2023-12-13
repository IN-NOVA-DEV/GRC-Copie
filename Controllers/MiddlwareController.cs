using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace grc_copie.Controllers
{
    public class Middleware : IActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Middleware(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (_httpContextAccessor.HttpContext.Session.GetString("username") == null)
            {
                // context.Result = new RedirectResult("~/Home/Error");
            }


        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Code exécuté après l'exécution de l'action
            //  Console.WriteLine("mivoaka");
        }
    }
}
