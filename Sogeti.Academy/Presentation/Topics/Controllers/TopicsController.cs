using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using Sogeti.Academy.Application.Topics.Queries.GetList;
using Sogeti.Academy.Presentation.General.Http;

namespace Sogeti.Academy.Presentation.Topics.Controllers
{
    [Route("topics")]
    public class TopicsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl;

        public TopicsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiUrl = configuration["Topics:ApiUrl"];
        }

        public async Task<IActionResult> Index()
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