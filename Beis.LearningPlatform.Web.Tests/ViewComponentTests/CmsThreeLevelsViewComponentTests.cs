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
    public class CmsThreeLevelsViewComponentTests : BaseViewComponentTest
    {
        private const string _copy1 = "Hello **strong** copy1";
        private const string _copy2 = "Hello **strong** copy2";
        private const string _copy3 = "Hello **strong** copy3";
        private const string _header1 = "Header 1";
        private const string _header2 = "Header 2";
        private const string _header3 = "Header 3";

        private MarkdownPipeline _markdownPipeline;

        [SetUp]
        public void Setup()
        {
            this._markdownPipeline = GetMarkdownPipeline();
        }

        private CmsThreeLevelsViewComponent CreateViewComponent()
        {
            return new CmsThreeLevelsViewComponent(_markdownPipeline);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Headers_And_No_Copy()
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
        public void Should_Not_Have_Content_If_No_Headers()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
                copy1 = _copy1,
                copy2 = _copy2,
                copy3 = _copy3
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
                header1 = _header1,
                header2 = _header2,
                header3 = _header3
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }


        [Test]
        public void Should_Have_Content_If_Copy_And_Headers()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.AreEqual(model.Component.copy1, _copy1);
            Assert.AreEqual(model.Component.copy2, _copy2);
            Assert.AreEqual(model.Component.copy3, _copy3);
            Assert.AreEqual(model.Component.header1, _header1);
            Assert.AreEqual(model.Component.header2, _header2);
            Assert.AreEqual(model.Component.header3, _header3);
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

            Assert.IsNotNull(model.HtmlCopy1);
            Assert.AreEqual(model.HtmlCopy1, "<p>Hello <strong>strong</strong> copy1</p>\n");
            
            Assert.IsNotNull(model.HtmlCopy2);
            Assert.AreEqual(model.HtmlCopy2, "<p>Hello <strong>strong</strong> copy2</p>\n");
            
            Assert.IsNotNull(model.HtmlCopy3);
            Assert.AreEqual(model.HtmlCopy3, "<p>Hello <strong>strong</strong> copy3</p>\n");

        }

   

        private static ViewDataDictionary<CmsThreeLevelsViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsThreeLevelsViewModel>;
            return viewComponentData;
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                copy1 = _copy1,
                copy2 = _copy2,
                copy3 = _copy3,
                header1 = _header1,
                header2 = _header2,
                header3 = _header3,
            };
        }
    }
}