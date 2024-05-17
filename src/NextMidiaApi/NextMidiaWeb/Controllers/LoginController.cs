using Microsoft.AspNetCore.Mvc;
using NextMidiaWeb.Domain.Entities;
using NextMidiaWeb.Models.Input;

namespace NextMidiaWeb.Controllers
{
    public class LoginController : Controller
    {
        #region Properties
        private readonly ILogger<LoginController> _logger;
        private readonly UsuarioService _service;
        #endregion

        #region Constructors
        public LoginController(ILogger<LoginController> logger, UsuarioService service)
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region Endpoints
        [Route("Login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Midia")]
        public IActionResult RedirectToMidia([FromForm] LoginInput input)
        {
            try
            {
                if (input.Email == null || input.Senha == null)
                    return Content("Preencha os dados para realizar o login.");

                var user = _service.FindByEmailAndSenha(input.Email, input.Senha);
                if (user != null)
                    return View("~/Views/Midia/Midia.cshtml"); // Para chamar uma view em um arquivo fora da pasta é necessário do caminho relativo na pasta

                return Content("Não foi encontrado nenhum login para os dados indicados.");
            }
            catch (Exception e)
            {
                return Content($"Erro no endpoint LoginController.Index: {e.Message}");
            }
        }
        #endregion
    }
}
