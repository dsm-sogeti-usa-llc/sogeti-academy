using Microsoft.AspNet.Mvc;

namespace Sogeti.Academy.Mvc.Tooling.Controllers
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
