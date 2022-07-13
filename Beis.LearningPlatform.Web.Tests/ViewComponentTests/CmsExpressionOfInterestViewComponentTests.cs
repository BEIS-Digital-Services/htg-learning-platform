namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsExpressionOfInterestViewComponentTests : BaseViewComponentTest
    {
        private const string _cTABannerText = "cTABannerText";
        private const string _cTAButtonText = "cTAButtonText";
        private const string _formHeader = "formHeader";
        private const string _formIntroMarkDown = "Hello **strong** copy1";
        private const string _thankYouHeader = "_thankYouHeader";
        private const string _thankYouTextMarkDown = "Hello **strong** copy2";
        private const string _pageName = "PageName";

        private MarkdownPipeline _markdownPipeline;

        [SetUp]
        public void Setup()
        {
            this._markdownPipeline = GetMarkdownPipeline();
        }

        private CmsExpressionOfInterestViewComponent CreateViewComponent()
        {
            return new CmsExpressionOfInterestViewComponent(_markdownPipeline);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Headers_And_No_Copy()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidPageViewModel(), new CMSPageComponent { });

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
            var view = component.Invoke(GetValidPageViewModel(), new CMSPageComponent
            {
                CTAButtonText = _cTAButtonText,
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
            var view = component.Invoke(GetValidPageViewModel(), new CMSPageComponent
            {
                CTABannerText = _cTABannerText,
                CTAButtonText = _cTAButtonText,
                FormHeader = _formHeader
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
            var view = component.Invoke(GetValidPageViewModel(), GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.AreEqual(model.CTABannerText, _cTABannerText);
            Assert.AreEqual(model.CTAButtonText, _cTAButtonText);
            Assert.AreEqual(model.FormHeader, _formHeader);
            Assert.AreEqual(model.FormIntro, _formIntroMarkDown);
            Assert.AreEqual(model.ThankYouHeader, _thankYouHeader);
            Assert.AreEqual(model.ThankYouText, _thankYouTextMarkDown);                            
        }


        [Test]
        public void Should_Convert_Markdown_If_Has_Content()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidPageViewModel(), GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.IsNotNull(model.FormIntroHtml);
            Assert.AreEqual(model.FormIntroHtml, "<p>Hello <strong>strong</strong> copy1</p>\n");

            Assert.IsNotNull(model.ThankYouTextHtml);
            Assert.AreEqual(model.ThankYouTextHtml, "<p>Hello <strong>strong</strong> copy2</p>\n");

        }



        private static ViewDataDictionary<CmsExpressionOfInterestViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsExpressionOfInterestViewModel>;
            return viewComponentData;
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                CTABannerText = _cTABannerText,
                CTAButtonText = _cTAButtonText,
                FormHeader = _formHeader,
                FormIntro = _formIntroMarkDown,
                ThankYouHeader = _thankYouHeader,
                ThankYouText = _thankYouTextMarkDown
            };
        }

        private static PageViewModel GetValidPageViewModel()
        {
            return new PageViewModel { pagename = _pageName };
        }
    }
}