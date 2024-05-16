using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NextMidiaWeb.Api.Models;
using NextMidiaWeb.Domain.Entities;
using NextMidiaWeb.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;

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

        [Route("")]        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Midia()
        {
            return View();
        }

        [HttpPost]
        [Route("")]        
        public IActionResult Index([FromForm] LoginInput input)
        {
            try
            {
                if(input.Email == null || input.Senha == null)
                    return Content("Preencha os dados para realizar o login.");

                var user = _service.FindByEmailAndSenha(input.Email, input.Senha);
                if (user != null)                
                    return View("Midia");
                                
                return PartialView("ErrorMessageView", new ErrorMessageViewModel { ErrorMessage = "Não foi encontrado nenhum login para os dados indicados.", StackTrace = null });
            }
            catch (Exception e)
            {
                return BadRequest();
            }          
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }    
    }
}
