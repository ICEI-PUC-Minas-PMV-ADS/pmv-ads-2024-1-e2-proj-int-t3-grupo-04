using Microsoft.AspNetCore.Mvc;
using NextMidiaWeb.Domain.Entities;
using NextMidiaWeb.Models.ViewModel;
using System.Diagnostics;

namespace NextMidiaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly UsuarioService _service;

        public HomeController(ILogger<HomeController> logger, UsuarioService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("UserId", null);
            //HttpContext.Session.SetString("UserUsername", null);
            //HttpContext.Session.SetString("UserPassword", null);
            return View();
        }

        [HttpGet]
        [Route("Login")]        
        public IActionResult RedirectToLogin(bool? param = false)
        {
            return View("~/Views/Login/Login.cshtml");
        }               

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }    
    }
}
