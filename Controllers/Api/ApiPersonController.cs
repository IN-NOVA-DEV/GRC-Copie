using Microsoft.AspNetCore.Mvc;

namespace grc_copie.Controllers.Api
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
