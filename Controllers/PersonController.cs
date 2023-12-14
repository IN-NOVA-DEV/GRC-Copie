using Microsoft.AspNetCore.Mvc;

namespace grc_copie.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
