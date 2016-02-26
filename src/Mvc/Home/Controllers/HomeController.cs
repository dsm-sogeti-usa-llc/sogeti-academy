using Microsoft.AspNet.Mvc;

namespace Sogeti.Academy.Mvc.Home.Controllers
{
    [Route("home")]
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
