using Microsoft.AspNet.Mvc;

namespace Sogeti.Academy.Presentation.Tooling.Controllers
{
    [Route("tooling")]
    public class ToolingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
