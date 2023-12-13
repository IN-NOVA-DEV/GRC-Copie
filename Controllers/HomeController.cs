using grc_copie.Controllers.Tools;
using grc_copie.Data;
using grc_copie.Service;
using GRC_Copie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GRC_Copie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger<HomeController> _logger;
        private readonly GRC_Context _context;
        private HomeService _hs;
        private IHttpContextAccessor inside;

        public HomeController(ILogger<HomeController> logger,GRC_Context bdd,HomeService homeservice, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;

            inside = httpContextAccessor;
            _context = bdd;
            _hs = homeservice;
            _logger = logger;
        }

        private IActionResult CheckJobIdAndRedirect()
        {
            string jobid = JobNameActionFilter.GetJobId(inside);

            if (string.IsNullOrEmpty(jobid))
                return RedirectToAction("Index", "Login");

            return null; // No redirection is needed
        }
        public IActionResult Index()
        {

            IActionResult redirectResult = CheckJobIdAndRedirect();
            if (redirectResult != null)
                return redirectResult;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}