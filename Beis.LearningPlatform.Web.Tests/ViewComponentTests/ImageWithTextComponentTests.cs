using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.ViewComponents;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class ImageWithTextComponentTests : BaseViewComponentTest
    {
        private const string ApiBaseUrl = "https://ApiBaseUrl";
        private const string ImageUrl = "/imageurl.jpg";
        private const string ImageAlternativeText = "alt text";
        private const string Copy = "Hello **strong** copy";

        private readonly IOptions<CmsOption> _cmsOptions = ConfigOptions.Create(new CmsOption { ApiBaseUrl = ApiBaseUrl });
        private MarkdownPipeline _markdownPipeline;

        [SetUp]
        public void Setup()
        {
            this._markdownPipeline = GetMarkdownPipeline();
        }

        private ImageWithTextViewComponent CreateViewComponent()
        {
            return new ImageWithTextViewComponent(_cmsOptions, _markdownPipeline);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Image_And_No_Copy()
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
        public void Should_Not_Have_Content_If_No_Image()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
                copy = Copy
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
                image = new CMSPageImage
                {
                    url = ImageUrl
                }
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Have_Content_If_Copy_And_Image()
        {

            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
        }

        [Test]
        public void Should_Have_Valid_Image_Url_If_Has_Content()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.IsNotNull(model.ImageUrl);
            
            Assert.IsTrue(model.ImageUrl.StartsWith(ApiBaseUrl));
            Assert.IsTrue(model.ImageUrl.EndsWith(ImageUrl));
        }

        [Test]
        public void Should_Map_Component_Image_AlternativeText_To_ImageAltText()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.IsNotNull(model.ImageAlt);
            Assert.AreEqual(model.ImageAlt, ImageAlternativeText);            
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

        private static ViewDataDictionary<ImageWithTextViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<ImageWithTextViewModel>;
            return viewComponentData;
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                copy = Copy,
                image = new CMSPageImage
                {
                    url = ImageUrl,
                    alternativeText = ImageAlternativeText
                }
            };
        }
    }
}