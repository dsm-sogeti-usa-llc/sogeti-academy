using System.Web.Mvc;

namespace Sogeti.Academy.Mvc.Tooling.Controllers
{
    [RoutePrefix("tooling")]
    public class ToolingController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
