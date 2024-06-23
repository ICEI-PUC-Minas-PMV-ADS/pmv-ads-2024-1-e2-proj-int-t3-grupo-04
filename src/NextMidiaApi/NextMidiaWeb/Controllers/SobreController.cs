using Microsoft.AspNetCore.Mvc;

namespace NextMidiaWeb.Controllers
{
    public class SobreController : Controller
    {
        [Route("Sobre")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
