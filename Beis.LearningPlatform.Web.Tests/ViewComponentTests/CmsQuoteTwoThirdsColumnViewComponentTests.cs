namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsQuoteTwoThirdsColumnViewComponentTests : BaseViewComponentTest
    {
        private const string _copy = "Hello **strong** copy";
        private const string _quote = "Quote **strong** text";
        private const string _quoteTwoThirdsAuthor = "quote column Author";
        private const AlignmentValue _quotePosition = AlignmentValue.Left;

        private MarkdownPipeline _markdownPipeline;

        [SetUp]
        public void Setup()
        {
            this._markdownPipeline = GetMarkdownPipeline();
        }

        private CmsQuoteTwoThirdsColumnViewComponent CreateViewComponent()
        {
            return new CmsQuoteTwoThirdsColumnViewComponent(_markdownPipeline);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Quote()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Have_Content_If_Quote()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.AreEqual(model.Component.quote, _quote);
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

            Assert.IsNotNull(model.HtmlQuote);
            Assert.AreEqual("<p>Quote <strong>strong</strong> text</p>\n", model.HtmlQuote);
        }

        [Test]
        public void Should_Have_TwoThirdsColumnAuthor_If_Author()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.IsTrue(model.HasQuoteTwoThirdsColumnAuthor);
        }

        [Test]
        public void Should_Not_Have_QuoteColumnAuthor_If_Author_Null()
        {
            var component = CreateViewComponent();
            var viewModel = GetValidCmsPageComponent();
            viewModel.quoteTwoThirdsAuthor = null;
            var view = component.Invoke(viewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.IsFalse(model.HasQuoteTwoThirdsColumnAuthor);
        }

        private static ViewDataDictionary<CmsQuoteTwoThirdsColumnViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsQuoteTwoThirdsColumnViewModel>;
            return viewComponentData;
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                quote = _quote,
                quoteTwoThirdsAuthor = _quoteTwoThirdsAuthor
            };
        }
    }
}