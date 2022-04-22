using Beis.LearningPlatform.Web.ControllerHelpers;
using Beis.LearningPlatform.Web.Controllers;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ServicesTests
{
    // NOTE: Check for SocketException: No such host is known to ensure strapi host exist
    public class CmsServiceTests
    {
        private Mock<ILogger<HomeControllerHelper>> _logger;
        private Mock<ILogger<HomeController>> _controllerLogger;
        private Mock<ICmsFeedbackService> _cmsFeedbackService;
        private Mock<ICmsService> _cmsService;
        private readonly IOptions<CmsOption> _cmsOptions = ConfigOptions.Create(new CmsOption());
        private Mock<IHttpContextAccessor> _httpContextAccessor;

        private HttpContext httpContext = new DefaultHttpContext();
        private Mock<ITempDataDictionary> _TempData;

        string Baseurl;
        
        private HomeController CreateController()
        {
            var homeControllerHelper = new HomeControllerHelper(_logger.Object,
                _cmsService.Object, 
                _cmsFeedbackService.Object,
                _cmsOptions, 
                _httpContextAccessor.Object);
            return new HomeController(_controllerLogger.Object, homeControllerHelper);
        }

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<HomeControllerHelper>>();
            _controllerLogger = new();
            _cmsFeedbackService = new();
            _cmsService = new Mock<ICmsService>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            httpContext.Request.Headers["Referer"] = "https://localhost";
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("localhost");
            Baseurl = httpContext.Request.Scheme + "://" + httpContext.Request.Host;

            _TempData = new Mock<ITempDataDictionary>();
        }

        [Test]
        public void Mock_httpContext_Returns_Scheme()
        {
            var httpContextScheme = httpContext.Request.Scheme;
            Assert.IsNotEmpty(httpContextScheme);
        }

        [Test]
        public void Mock_httpContext_Returns_Host()
        {
            var httpContextHost = httpContext.Request.Host.Host;
            Assert.IsNotEmpty(httpContextHost);
        }
    }
}