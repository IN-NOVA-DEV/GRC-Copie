using grc_copie.Controllers.Tools;
using grc_copie.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace grc_copie.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly GRC_Context _context;
        public LoginController(GRC_Context context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.GetDisplayName();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.Session.SetString("username", username);

                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
                if (roleClaim != null)
                {
                    string roleValue = roleClaim.Value;
                    HttpContext.Session.SetString("role", roleValue);
                    Debug.WriteLine(roleValue);
                    var claims = new List<Claim>
{
                      // Autres claims de l'utilisateur (nom, e-mail, etc.)
                       new Claim(ClaimTypes.Role, roleValue) // Ajouter le claim de rôle "admin"
                        };

                    var identity = new ClaimsIdentity(claims, "JWT");

                    var Organisation = _context.Organisations.ToList();
                    var Jobs = _context.Jobs.ToList();

                    //LoginView logView = new LoginView // A ajuster en fonction de la methode de vue utiliser
                    //{
                    //    JobsList = Jobs,
                    //    OrganisationsList = Organisation
                    //};
                    ////    HttpContext.Session.SetString("identity", identity.Name);
                    //return View(logView);
                    //// Utilisez la valeur du rôle selon vos besoins
                }


            }


            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImplementJobCategory(string jobId)
        {
            if (jobId == null || jobId == "") return RedirectToAction("Index", "Login");
            JobNameActionFilter.SetJobID(HttpContext, jobId, _context);
            var listeJobs = _context.Jobs.ToList();
            string jobs = JsonConvert.SerializeObject(listeJobs);
            HttpContext.Session.SetString("listeJobs", jobs);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/Login/RedirectToImplementJob/{jobId}")]
        public IActionResult RedirectToImplementJob(string jobId)
        {
            Debug.WriteLine(jobId);
            if (jobId == null || jobId == "") return RedirectToAction("Index", "Login");
            JobNameActionFilter.SetJobID(HttpContext, jobId, _context);

            var listeJobs = _context.Jobs.ToList();
            string jobs = JsonConvert.SerializeObject(listeJobs);
            HttpContext.Session.SetString("listeJobs", jobs);
            return RedirectToAction("Index", "Home");
        }

    }
}
