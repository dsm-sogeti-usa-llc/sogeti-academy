using Microsoft.AspNet.Mvc;
using Sogeti.Academy.Mvc.Home.Controllers;
using Xunit;

namespace Mvc.Test.Home.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController _homeController;

        public HomeControllerTests()
        {
            _homeController = new HomeController();
        }

        [Fact]
        public void Index_ShouldReturnView()
        {
            var result = _homeController.Index();
            Assert.IsType<ViewResult>(result);
        }
    }
}
