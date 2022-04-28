using Beis.LearningPlatform.Web.ControllerHelpers;
using Beis.LearningPlatform.Web.Controllers;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class HomeControllerTests
    {
        private Mock<ICmsFeedbackService> _cmsFeedbackService;
        private Mock<ICmsService> _cmsService;
        private Mock<ILogger<HomeController>> _controllerLogger;
        private Mock<ILogger<HomeControllerHelper>> _logger;
        private readonly IOptions<CmsOption> _cmsOptions = ConfigOptions.Create(new CmsOption());
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private const string InvalidUrl = "URL-NOT-IN-CMS";

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
            _cmsFeedbackService = new();
            _cmsService = new();
            _logger = new();
            _controllerLogger = new();
            _httpContextAccessor = new();
            _cmsService.Setup(m => m.GetCustomPageResult(It.IsAny<string>()))
                        .ReturnsAsync((string cmsAction) => new CMSPageViewModel
                        {
                            id = cmsAction.EndsWith(InvalidUrl, StringComparison.OrdinalIgnoreCase) ? 0 : 1,
                            components = new List<CMSPageComponent>()
                        });
        }

        [TestCase("buying-with-confidence")]
        [TestCase("buying-with-confidence/#feedback-prompt")]
        [TestCase("buy-software-with-confidence")]
        [TestCase("buy-software-with-confidence/#feedback-prompt")]
        [TestCase("calculate-your-software-budget")]
        [TestCase("calculate-your-software-budget/#feedback-prompt")]
        [TestCase("calculating-your-budget")]
        [TestCase("calculating-your-budget/#feedback-prompt")]
        [TestCase("choosing-the-right-software-for-your-business")]
        [TestCase("choosing-the-right-software-for-your-business/#feedback-prompt")]
        [TestCase("crannis-technology-services")]
        [TestCase("crannis-technology-services/#feedback-prompt")]
        [TestCase("creating-your-shopping-list")]
        [TestCase("creating-your-shopping-list/#feedback-prompt")]
        [TestCase("cyber-security")]
        [TestCase("cyber-security/#feedback-prompt")]
        [TestCase("digital-accounting-copper-connexions")]
        [TestCase("digital-accounting-copper-connexions/#feedback-prompt")]
        [TestCase("dmitry-test")]
        [TestCase("dmitry-test/#feedback-prompt")]
        [TestCase("ecommerce-brightside-brewery")]
        [TestCase("ecommerce-brightside-brewery/#feedback-prompt")]
        [TestCase("ecommerce-brightside-brewing-company")]
        [TestCase("ecommerce-brightside-brewing-company/#feedback-prompt")]
        [TestCase("ecommerce-dunsters-farm")]
        [TestCase("ecommerce-dunsters-farm/#feedback-prompt")]
        [TestCase("eligibility")]
        [TestCase("eligibility/#feedback-prompt")]
        [TestCase("get-in-touch")]
        [TestCase("get-in-touch/#feedback-prompt")]
        [TestCase("getting-your-moneys-worth")]
        [TestCase("getting-your-moneys-worth/#feedback-prompt")]
        [TestCase("get-your-moneys-worth")]
        [TestCase("get-your-moneys-worth/#feedback-prompt")]
        [TestCase("how-to-compare-products")]
        [TestCase("how-to-compare-products/#feedback-prompt")]
        [TestCase("how-to-recognise-your-business-needs")]
        [TestCase("how-to-recognise-your-business-needs/#feedback-prompt")]
        [TestCase("introduction-to-accounting-software")]
        [TestCase("introduction-to-accounting-software/#feedback-prompt")]
        [TestCase("introduction-to-crm-software")]
        [TestCase("introduction-to-crm-software/#feedback-prompt")]
        [TestCase("introduction-to-ecommerce")]
        [TestCase("introduction-to-ecommerce/#feedback-prompt")]
        [TestCase("lawrence-test")]
        [TestCase("lawrence-test/#feedback-prompt")]
        [TestCase("make-new-software-work-for-your-team")]
        [TestCase("make-new-software-work-for-your-team/#feedback-prompt")]
        [TestCase("making-it-work-for-the-team")]
        [TestCase("making-it-work-for-the-team/#feedback-prompt")]
        [TestCase("pure-punjabi")]
        [TestCase("pure-punjabi/#feedback-prompt")]
        [TestCase("review-your-software-post-launch")]
        [TestCase("review-your-software-post-launch/#feedback-prompt")]
        [TestCase("set-objectives-for-new-software")]
        [TestCase("set-objectives-for-new-software/#feedback-prompt")]
        [TestCase("six-steps-to-choosing-software")]
        [TestCase("six-steps-to-choosing-software/#feedback-prompt")]
        [TestCase("techdisinfect")]
        [TestCase("techdisinfect/#feedback-prompt")]
        [TestCase("terms-and-conditions")]
        [TestCase("terms-and-conditions/#feedback-prompt")]
        [TestCase("the-benefits-of-digital-to-your-business")]
        [TestCase("the-benefits-of-digital-to-your-business/#feedback-prompt")]
        [TestCase("would-crm-suit-my-business")]
        [TestCase("would-crm-suit-my-business/#feedback-prompt")]
        [TestCase("would-digital-accounting-suit-my-business")]
        [TestCase("would-digital-accounting-suit-my-business/#feedback-prompt")]
        [TestCase("write-your-requirements-list")]
        [TestCase("write-your-requirements-list/#feedback-prompt")]
        [Test]
        public async Task DynamicMapping_ValidRoutes_MapToStrapiApiTarget(string strapiAction)
        {
            var controller = CreateController();
            string strapiActionTarget = $"Custom-pages/{strapiAction}";

            var result = await controller.CMSCustomPagesRoute(strapiAction);
            Assert.IsNotNull(result);
            Assert.That(result is ViewResult);

            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewName == "Resources");
            Assert.IsNotNull(viewResult.Model);
            Assert.That(viewResult.Model is CMSPageViewModel);
            _cmsService.Verify(m => m.GetCustomPageResult(strapiActionTarget));
        }

        [TestCase("would-crm-suit-my-business")]
        [TestCase("write-your-requirements-list")]
        [Test]
        public async Task Preview_ValidRoutes_MapToStrapiApiTarget(string strapiAction)
        {
            var controller = CreateController();
            string strapiActionTarget = $"Custom-pages/preview/{strapiAction}";

            var result = await controller.CMSPreviewCustomPage(strapiAction);
            Assert.IsNotNull(result);
            Assert.That(result is ViewResult);

            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewName == "Resources");
            Assert.IsNotNull(viewResult.Model);
            Assert.That(viewResult.Model is CMSPageViewModel);
            _cmsService.Verify(m => m.GetCustomPageResult(strapiActionTarget));
        }

        [Test]
        public async Task DynamicMapping_InValid_Cms_Action_Should_Return_Error_Page_Not_Found()
        {
            var controller = CreateController();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var strapiActionTarget = $"Custom-pages/{InvalidUrl}";

            var result = await controller.CMSCustomPagesRoute(InvalidUrl);
            Assert.IsNotNull(result);
            Assert.That(result is ViewResult);

            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewName == "Error");
            Assert.IsNotNull(viewResult.Model);
            var errorViewModel = viewResult.Model as ErrorViewModel;
            Assert.IsNotNull(errorViewModel);
            Assert.AreEqual(errorViewModel.title, "Page not found");
            Assert.AreEqual(errorViewModel.description, "We could not find the page you requested");

            _cmsService.Verify(m => m.GetCustomPageResult(strapiActionTarget));
        }


        [Test]
        [TestCase(null)]
        [TestCase("crm")]
        public async Task Test_HomeController_Guidance_and_tools_redirects_tag(string tag)
        {
            var controller = CreateController();

            var result = await controller.Guidance_and_tools(tag, null);
            Assert.IsNotNull(result);
            Assert.That((string.IsNullOrWhiteSpace(tag) && result is ViewResult) || result is IActionResult);
        }

        [Test]
        [TestCase(null)]
        [TestCase(true)]
        [TestCase(false)]
        public async Task Test_HomeController_Guidance_and_tools_Sets_PreviewSearchArticles(bool? previewSearchArticles)
        {
            var controller = CreateController();

            var result = await controller.Guidance_and_tools(null, previewSearchArticles) as ViewResult;
            Assert.IsNotNull(result);
            var cmsPageViewModel = result.Model as CMSPageViewModel;
            Assert.IsNotNull(cmsPageViewModel);
            Assert.AreEqual(previewSearchArticles, cmsPageViewModel.PreviewSearchArticles);
        }

        [Test]
        public void Test_HomeController_Comparison_Tool_redirects_ComparisonTool_Start()
        {
            var controller = CreateController();

            var result = controller.Comparison_Tool() as RedirectToActionResult;
            Assert.IsNotNull(result);
            Assert.True(result.ControllerName == "ComparisonTool");
            Assert.True(result.ActionName == "Start");
        }

        [Test]
        public void Test_HomeController_Comparison_ToolNoJs_redirects_ComparisonTool_StartNoJs()
        {
            var controller = CreateController();

            var result = controller.Comparison_ToolNoJs() as RedirectToActionResult;
            Assert.IsNotNull(result);
            Assert.True(result.ControllerName == "ComparisonTool");
            Assert.True(result.ActionName == "StartNoJs");
        }
    }
}