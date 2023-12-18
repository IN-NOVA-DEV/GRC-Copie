using Microsoft.AspNetCore.Mvc;

namespace grc_copie.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
