namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsQuoteCalloutViewComponentTests : BaseViewComponentTest
    {
        private const string _copy = "Hello **strong** copy";
        private const string _quote = "Quote **strong** text";
        private const string _quoteCalloutAuthor = "quote Callout Author";
        private const AlignmentValue _quotePosition = AlignmentValue.Left;

        private MarkdownPipeline _markdownPipeline;

        [SetUp]
        public void Setup()
        {
            this._markdownPipeline = GetMarkdownPipeline();
        }

        private CmsQuoteCalloutViewComponent CreateViewComponent()
        {
            return new CmsQuoteCalloutViewComponent(_markdownPipeline);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Quote_And_No_Copy()
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
        public void Should_Not_Have_Content_If_No_Quote()
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
                quote = _quote
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_QuotePosition()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
                copy = _copy,
                quote = _quote
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Have_Content_If_Copy_And_Quote_And_Position()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.AreEqual(model.Component.copy, _copy);
            Assert.AreEqual(model.Component.quote, _quote);
            Assert.AreEqual(model.Component.quotePosition, _quotePosition);
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

            Assert.IsNotNull(model.HtmlQuote);
            Assert.AreEqual(model.HtmlQuote, "<blockquote>Quote <strong>strong</strong> text</blockquote>\n");
        }

        [Test]
        public void Should_Have_QuoteCalloutAuthor_If_Author()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.IsTrue(model.HasQuoteCalloutAuthor);
        }

        [Test]
        public void Should_Not_Have_QuoteCalloutAuthor_If_Author_Null()
        {
            var component = CreateViewComponent();
            var viewModel = GetValidCmsPageComponent();
            viewModel.quoteCalloutAuthor = null;
            var view = component.Invoke(viewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.IsFalse(model.HasQuoteCalloutAuthor);
        }

        private static ViewDataDictionary<CmsQuoteCalloutViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsQuoteCalloutViewModel>;
            return viewComponentData;
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                copy = _copy,
                quote = _quote,
                quotePosition = _quotePosition,
                quoteCalloutAuthor = _quoteCalloutAuthor
            };
        }
    }
}