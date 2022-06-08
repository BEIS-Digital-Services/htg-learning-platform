using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsCaseStudyViewComponentTests : BaseViewComponentTest
    {
        private MarkdownPipeline _markdownPipeline;
        private const string ApiBaseUrl = "https://ApiBaseUrl";
        private readonly IOptions<CmsOption> _cmsOptions = ConfigOptions.Create(new CmsOption { ApiBaseUrl = ApiBaseUrl });

        [SetUp]
        public void Setup()
        {
            _markdownPipeline = GetMarkdownPipeline();
        }

        private CmsCaseStudyViewComponent CreateViewComponent()
        {
            return new CmsCaseStudyViewComponent(_cmsOptions, _markdownPipeline);
        }

        [Test]
        public void Should_Not_Have_Content_If_no_content()
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
            Assert.AreEqual("<p>copy</p>\n", model.HtmlCopy);
            Assert.IsNotNull(model.ImageUrl);
            
        }


        private static ViewDataDictionary<CmsCaseStudyViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsCaseStudyViewModel>;
            return viewComponentData;
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                content = new CMSPageContent
                {
                    header = "header",
                    subheader = "subheader",
                    copy ="copy"
                },
                image = new CMSPageImage
                {
                    url = "testUrl"
                },
            };
        }
    }
}