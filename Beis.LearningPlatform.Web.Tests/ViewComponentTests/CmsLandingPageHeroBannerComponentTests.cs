namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsLandingPageHeroBannerComponentTests : BaseViewComponentTest
    {
        private MarkdownPipeline _markdownPipeline;

        private CmsLandingPageHeroBannerViewComponent CreateViewComponent()
        {
            return new CmsLandingPageHeroBannerViewComponent(_markdownPipeline);
        }

        [SetUp]
        public void Setup()
        {
            this._markdownPipeline = GetMarkdownPipeline();
        }

        private static CMSPageComponent GetValidCmsComponent()
        {
            return new CMSPageComponent
            {
                header = "Header",
                intro = "Intro",
                image = new CMSPageImage
                {
                    url = "imageUrl"
                }
            };
        }

        [Test]
        public void Should_Not_Have_Content_If_NoViewModelData()
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

            var invalidComponent = GetValidCmsComponent();
            invalidComponent.header = null;

            var view = component.Invoke(invalidComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Intro()
        {
            var component = CreateViewComponent();

            var invalidComponent = GetValidCmsComponent();
            invalidComponent.intro = string.Empty;

            var view = component.Invoke(invalidComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_IntroHtml()
        {
            var component = CreateViewComponent();

            var invalidComponent = GetValidCmsComponent();
            invalidComponent.intro = string.Empty;

            var view = component.Invoke(invalidComponent);

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

            var invalidComponent = GetValidCmsComponent();
            invalidComponent.image = null;

            var view = component.Invoke(invalidComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }


        [Test]
        public void Should_Have_Content_If_ValidModel()
        {
            var component = CreateViewComponent();

            var validComponent = GetValidCmsComponent();
            var view = component.Invoke(validComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
        }


        private static ViewDataDictionary<CmsLandingPageHeroBannerViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsLandingPageHeroBannerViewModel>;
            return viewComponentData;
        }

    }
}