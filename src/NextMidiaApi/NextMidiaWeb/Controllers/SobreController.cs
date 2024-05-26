using Microsoft.AspNetCore.Mvc;

namespace NextMidiaWeb.Controllers
{
    public class SobreController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
