using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.ViewComponents;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
	public class SiteNavigationViewComponentTests : BaseViewComponentTest
    {        
        private readonly Mock<ICmsService> _cmsService = new();
        private readonly Mock<IPageViewModel> _pageViewModel = new();

        [SetUp]
        public void Setup()
		{
			IEnumerable<SiteNavigationModel> navList =
				JsonConvert.DeserializeObject<List<SiteNavigationModel>>(_cmsNavigationJson);
			_cmsService.Setup(x => x.GetSiteNavigation()).Returns(Task.FromResult(navList));

		}

		[Test]
        public async Task Should_Return_Valid_SiteNavigationModel()
        {
            var httpContextAccessor = GetHttpContextAccessor("/");

            var siteNavigationViewComponent = new SiteNavigationViewComponent(_cmsService.Object, httpContextAccessor.Object);
            var viewModel = await siteNavigationViewComponent.InvokeAsync(_pageViewModel.Object);
            Assert.IsNotNull(viewModel);

            var viewComponentResult = viewModel as Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult;
            Assert.IsNotNull(viewComponentResult);
            Assert.IsNotNull(viewComponentResult.ViewData);

            var viewData = viewComponentResult.ViewData as Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<SiteNavigationViewModel>;
            Assert.IsNotNull(viewData);            
        }

		[Test]
        [TestCase(23, "/about")]
        [TestCase(43, "/guidance-and-tools")]
        [TestCase(21, "/home")]
        public async Task Should_Return_Correct_SiteNavigationModel_Active_ForCmsPageRequest(int selectedId, string requestPath)
        {
            var httpContextAccessor = GetHttpContextAccessor(requestPath);
            _pageViewModel.SetupGet(x => x.id).Returns(selectedId);

            var siteNavigationViewComponent = new SiteNavigationViewComponent(_cmsService.Object, httpContextAccessor.Object);
            var viewModel = await siteNavigationViewComponent.InvokeAsync(_pageViewModel.Object);
            Assert.IsNotNull(viewModel);

            var viewComponentResult = viewModel as Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult;
            Assert.IsNotNull(viewComponentResult);
            Assert.IsNotNull(viewComponentResult.ViewData);

            var viewData = viewComponentResult.ViewData as Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<SiteNavigationViewModel>;
            Assert.IsNotNull(viewData);

            var allNavItems = viewData.Model.SiteNavigationModels
                .SelectMany(x => x.SubNavigationItems)
                .ToList();
            allNavItems.AddRange(viewData.Model.SiteNavigationModels.Select(x => x.NavigationItem));

            Assert.IsTrue(allNavItems.Any());

            var selectedItems = allNavItems.Where(x => x.PageViewModel?.id == selectedId);

            Assert.IsTrue(selectedItems.All(x => x.IsActive));
        }

		[Test]
        [TestCase(15, "/diagnostic-tool/start")]
        [TestCase(15, "/diagnostic-tool/nextstep")]
        [TestCase(16, "/comparison-tool")]
        [TestCase(16, "/comparison-tool/compare")]
        public async Task Should_Return_Correct_SiteNavigationModel_Active_ForExternalPageRequest(int selectedId, string requestPath)
        {
            var httpContextAccessor = GetHttpContextAccessor(requestPath);

            var siteNavigationViewComponent = new SiteNavigationViewComponent(_cmsService.Object, httpContextAccessor.Object);
            var viewModel = await siteNavigationViewComponent.InvokeAsync(_pageViewModel.Object);
            Assert.IsNotNull(viewModel);

            var viewComponentResult = viewModel as Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult;
            Assert.IsNotNull(viewComponentResult);
            Assert.IsNotNull(viewComponentResult.ViewData);

            var viewData = viewComponentResult.ViewData as Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<SiteNavigationViewModel>;
            Assert.IsNotNull(viewData);

            var allNavItems = viewData.Model.SiteNavigationModels
                .SelectMany(x => x.SubNavigationItems)
                .ToList();
            allNavItems.AddRange(viewData.Model.SiteNavigationModels.Select(x => x.NavigationItem));

            Assert.IsTrue(allNavItems.Any());

            var selectedItems = allNavItems.Where(x => x.id == selectedId);

            Assert.IsTrue(selectedItems.All(x => x.IsActive));
        }

       
		private Mock<IHttpContextAccessor> GetHttpContextAccessor(string requestPath)
		{
			Mock<IHttpContextAccessor> httpContextAccessor = new();
			Mock<HttpContext> httpContext = new();
			Mock<HttpRequest> httpRequest = new();

			httpRequest.SetupGet(x => x.Path)
				.Returns(new PathString(requestPath));
			httpContext.SetupGet(x => x.Request)
				.Returns(httpRequest.Object);
			httpContextAccessor.SetupGet(x => x.HttpContext)
				.Returns(httpContext.Object);

            return httpContextAccessor;
		}

        private const string _cmsNavigationJson = @"
            [
                {
                    ""id"": 7,
                    ""Description"": ""Guidance and tools"",
                    ""NavigationItem"": {
                        ""id"": 14,
                        ""Title"": ""Guidance and tools"",
                        ""Url"": null,
                        ""cssClass"": null,
                        ""ariaText"": null,
                        ""custom_page"": {
                            ""id"": 43,
                            ""pagename"": ""guidance-and-tools"",
                        },
                        ""ExternalUrl"": null,
                        ""UrlRegEx"": null,
                        ""CssClass"": null,
                        ""AriaText"": null,
                        ""Order"": 30
                    },
                    ""SubNavigationItems"": []
                },
                {
                    ""id"": 8,
                    ""Description"": ""Find your software"",
                    ""NavigationItem"": {
                        ""id"": 15,
                        ""Title"": ""Find your software"",
                        ""Url"": ""/diagnostic-tool/start"",
                        ""cssClass"": null,
                        ""ariaText"": null,
                        ""custom_page"": null,
                        ""ExternalUrl"": null,
                        ""UrlRegEx"": ""\/diagnostic-tool\/.+"",
                        ""CssClass"": null,
                        ""AriaText"": null,
                        ""Order"": 40
                    },
                    ""SubNavigationItems"": []
                },
                {
                    ""id"": 6,
                    ""Description"": ""About"",
                    ""NavigationItem"": {
                        ""id"": 11,
                        ""Title"": ""About"",
                        ""Url"": null,
                        ""cssClass"": null,
                        ""ariaText"": null,
                        ""custom_page"": {
                            ""id"": 23,
                            ""pagename"": ""about"",
                        },
                        ""ExternalUrl"": null,
                        ""UrlRegEx"": null,
                        ""CssClass"": null,
                        ""AriaText"": null,
                        ""Order"": 20
                    },
                    ""SubNavigationItems"": [
                        {
                            ""id"": 12,
                            ""Title"": ""About sub 1"",
                            ""Url"": null,
                            ""cssClass"": null,
                            ""ariaText"": null,
                            ""custom_page"": {
                                ""id"": 83,
                                ""pagename"": ""someone-test"",
                            },
                            ""ExternalUrl"": null,
                            ""UrlRegEx"": null,
                            ""CssClass"": null,
                            ""AriaText"": null,
                            ""Order"": 10
                        },
                        {
                            ""id"": 13,
                            ""Title"": ""About sub 2"",
                            ""Url"": null,
                            ""cssClass"": null,
                            ""ariaText"": null,
                            ""custom_page"": {
                                ""id"": 23,
                                ""pagename"": ""about"",
                            },
                            ""ExternalUrl"": ""/about-ext/link"",
                            ""UrlRegEx"": null,
                            ""CssClass"": null,
                            ""AriaText"": null,
                            ""Order"": 20
                        }
                    ]
                },
                {
                    ""id"": 9,
                    ""Description"": ""Get your software discount"",
                    ""NavigationItem"": {
                        ""id"": 16,
                        ""Title"": ""Get your software discount"",
                        ""Url"": ""/comparison-tool"",
                        ""cssClass"": null,
                        ""ariaText"": null,
                        ""custom_page"": null,
                        ""ExternalUrl"": null,
                        ""UrlRegEx"": ""\/comparison-tool\/?.*"",
                        ""CssClass"": null,
                        ""AriaText"": null,
                        ""Order"": 50
                    },
                    ""SubNavigationItems"": []
                },
                {
                    ""id"": 5,
                    ""Description"": ""Home"",
                    ""NavigationItem"": {
                        ""id"": 8,
                        ""Title"": ""Home"",
                        ""Url"": """",
                        ""cssClass"": null,
                        ""ariaText"": null,
                        ""custom_page"": {
                            ""id"": 21,
                            ""pagename"": ""home"",
                        },
                        ""ExternalUrl"": null,
                        ""UrlRegEx"": null,
                        ""CssClass"": null,
                        ""AriaText"": null,
                        ""Order"": 10
                    },
                    ""SubNavigationItems"": []
                }
            ]

";
    }
}