using Microsoft.AspNetCore.Mvc;

namespace grc_copie.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult UserActivity()
        {
            return View();
        }

        public IActionResult AllActivity()
        {
            return View();
        }
    }
}
