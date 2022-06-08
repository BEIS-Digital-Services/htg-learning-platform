using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.ViewComponents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsSearchArticleListingViewComponentTests : BaseViewComponentTest
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
        private readonly Mock<HttpContext> _httpContext = new();
        private readonly Mock<HttpRequest> _httpRequest = new();
        private readonly Mock<IPageViewModel> _pageViewModel = new();
        private const string PageTitle = "Help to Grow: Digital - the benefits of digital to your business";
        private const string Path = "/the-benefits-of-digital-to-your-business";
        private readonly Mock<ICmsService> _cmsService = new();

        [SetUp]
        public async Task Setup()
        {
            _httpRequest.SetupGet(x => x.Path)
                .Returns(Path);
            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpContextAccessor.SetupGet(x => x.HttpContext)
               .Returns(_httpContext.Object);
            _pageViewModel.SetupGet(x => x.pageTitle)
                .Returns(PageTitle);
        }

        private CmsSearchArticleListingViewComponent CreateViewComponent()
        {
            return new CmsSearchArticleListingViewComponent(_httpContextAccessor.Object, _cmsService.Object);
        }

        private static CMSPageComponent GetValidCmsComponent()
        {
            return new CMSPageComponent
            {
                SearchArticles = new List<CMSSearchArticlePicker> {
                     new CMSSearchArticlePicker{ id = 1 },
                     new CMSSearchArticlePicker{ id = 2 },
                     new CMSSearchArticlePicker{ id = 3 }
                }
            };
        }

        [Test]
        public async Task Should_Not_Have_Content_If_NoViewModelData()
        {
            var component = CreateViewComponent();
            var view = await component.InvokeAsync(new CMSPageComponent { });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public async Task Should_Not_Have_Content_If_No_SearchArticle()
        {
            var component = CreateViewComponent();

            var invalidComponent = GetValidCmsComponent();
            invalidComponent.SearchArticles = null;

            var view = await component.InvokeAsync(invalidComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public async Task Should_Have_Content_If_ValidModel()
        {
            var component = CreateViewComponent();

            var validComponent = GetValidCmsComponent();
            var view = await component.InvokeAsync(validComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
        }


        private static ViewDataDictionary<CmsLinkListHeroViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsLinkListHeroViewModel>;
            return viewComponentData;
        }

    }
}