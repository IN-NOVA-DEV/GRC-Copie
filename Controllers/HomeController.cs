using grc_copie.Controllers.Tools;
using grc_copie.Data;
using grc_copie.Models;
using grc_copie.Service;
using GRC_Copie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

       public IActionResult Index()
        {
            return View();  // nom de la vue à mettre 
        }

        [Authorize(Policy = "Admin")]
        public ViewResult ListActivity()
        {
            return View();
        }
        public ViewResult Schedule()
        {
            return View();
        }
        public ViewResult AllSchedule()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}