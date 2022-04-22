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
    public class CmsTopTipViewComponentTests : BaseViewComponentTest
    {
        private const string _copy = "Hello **strong** copy";
        private const string _header = "Header text";

        private MarkdownPipeline _markdownPipeline;

        [SetUp]
        public void Setup()
        {
            this._markdownPipeline = GetMarkdownPipeline();

        }

        private CmsTopTipViewComponent CreateViewComponent()
        {
            return new CmsTopTipViewComponent(_markdownPipeline);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Header_And_No_Copy()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent { });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Header()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
                copy = _copy
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Copy()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
                header = _header
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Have_Content_If_Copy_And_Header()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.AreEqual(model.Component.copy, _copy);
            Assert.AreEqual(model.Component.header, _header);
        }


        [Test]
        public void Should_Convert_Markdown_If_Has_Content()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.IsNotNull(model.HtmlCopy);
            
            Assert.AreEqual(model.HtmlCopy, "<p>Hello <strong>strong</strong> copy</p>\n");
        }

        private static ViewDataDictionary<CmsTopTipViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsTopTipViewModel>;
            return viewComponentData;
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                copy = _copy,
                header = _header
            };
        }
    }
}