using System.Web.Mvc;

namespace Sogeti.Academy.Mvc.Home.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("home")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
