using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.ViewComponents;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NUnit.Framework;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsCardComponentTests : BaseViewComponentTest
    {
        private MarkdownPipeline _markdownPipeline;
        [SetUp]
        public void Setup()
        {
            _markdownPipeline = GetMarkdownPipeline();

        }
        private CmsCardViewComponent CreateViewComponent()
        {
            return new CmsCardViewComponent(_markdownPipeline);
        }

        [Test]
        public void Should_Return_Valid_View_And_Data()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.AreEqual("<p>one</p>\n", model.One);
            Assert.AreEqual("<p>two</p>\n", model.Two);
            Assert.AreEqual("<p>three</p>\n", model.Three);
            Assert.AreEqual("<p>four</p>\n", model.Four);
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                one = "one",
                two = "two",
                three = "three",
                four = "four",
            };
        }

        private static ViewDataDictionary<CmsCardViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsCardViewModel>;
            return viewComponentData;
        }
    }
}