using System.Threading.Tasks;
using System.Web.Mvc;
using Sogeti.Academy.Application.Topics.Queries.GetList;
using Sogeti.Academy.Infrastructure.Configuration;
using Sogeti.Academy.Mvc.General.Http;

namespace Sogeti.Academy.Mvc.Topics.Controllers
{
    [RoutePrefix("topics")]
    public class TopicsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl;

        public TopicsController()
            : this(new HttpClientFactory(), new Configuration())
        {
            
        }

        public TopicsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiUrl = configuration["Topics:ApiUrl"];
        }

        [Route("")]
        public async Task<ActionResult> Index()
        {
            using (var client = _httpClientFactory.Create())
            {
                ViewBag.Title = "Topics";
                var viewModel = await client.GetJson<ListViewModel>($"{_apiUrl}/topics");
                return View(viewModel);
            }
        }
    }
}