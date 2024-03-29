﻿namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsNavigationBreadcrumbViewComponentTests : BaseViewComponentTest
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

        private static CMSPageComponent GetValidCmsComponent()
        {
            return new CMSPageComponent
            {
                AdditionalLinks = new List<CMSSimpleLink>() {
                     new CMSSimpleLink{
                        id = 1,
                        LinkText = "LinkText1",
                        LinkUrl = "LinkUrl1"
                    },
                    new CMSSimpleLink{
                        id = 2,
                        LinkText = "LinkText2",
                        LinkUrl = "LinkUrl2"
                    }
                 }
            };
        }


		[Test]
        public async Task Should_Return_Valid_SiteNavigationModel()
        {
            var httpContextAccessor = GetHttpContextAccessor("/");

            var CmsNavigationBreadcrumbViewComponent = new CmsNavigationBreadcrumbViewComponent(_cmsService.Object, httpContextAccessor.Object);
            var viewModel = await CmsNavigationBreadcrumbViewComponent.InvokeAsync(_pageViewModel.Object, GetValidCmsComponent());
            Assert.IsNotNull(viewModel);

            var viewComponentResult = viewModel as Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult;
            Assert.IsNotNull(viewComponentResult);
            Assert.IsNotNull(viewComponentResult.ViewData);

            var viewData = viewComponentResult.ViewData as Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<NavigationBreadcrumbViewModel>;
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

            var CmsNavigationBreadcrumbViewComponent = new CmsNavigationBreadcrumbViewComponent(_cmsService.Object, httpContextAccessor.Object);
            var viewModel = await CmsNavigationBreadcrumbViewComponent.InvokeAsync(_pageViewModel.Object, GetValidCmsComponent());
            Assert.IsNotNull(viewModel);

            var viewComponentResult = viewModel as Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult;
            Assert.IsNotNull(viewComponentResult);
            Assert.IsNotNull(viewComponentResult.ViewData);

            var viewData = viewComponentResult.ViewData as Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<NavigationBreadcrumbViewModel>;
            Assert.IsNotNull(viewData);

            var allNavItems = viewData.Model.SiteNavigationModel.SiteNavigationModels
                .SelectMany(x => x.SubNavigationItems)
                .ToList();
            allNavItems.AddRange(viewData.Model.SiteNavigationModel.SiteNavigationModels.Select(x => x.NavigationItem));

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

            var CmsNavigationBreadcrumbViewComponent = new CmsNavigationBreadcrumbViewComponent(_cmsService.Object, httpContextAccessor.Object);
            var viewModel = await CmsNavigationBreadcrumbViewComponent.InvokeAsync(_pageViewModel.Object, GetValidCmsComponent());
            Assert.IsNotNull(viewModel);

            var viewComponentResult = viewModel as Microsoft.AspNetCore.Mvc.ViewComponents.ViewViewComponentResult;
            Assert.IsNotNull(viewComponentResult);
            Assert.IsNotNull(viewComponentResult.ViewData);

            var viewData = viewComponentResult.ViewData as Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<NavigationBreadcrumbViewModel>;
            Assert.IsNotNull(viewData);

            var allNavItems = viewData.Model.SiteNavigationModel.SiteNavigationModels
                .SelectMany(x => x.SubNavigationItems)
                .ToList();
            allNavItems.AddRange(viewData.Model.SiteNavigationModel.SiteNavigationModels.Select(x => x.NavigationItem));

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