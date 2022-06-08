namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsStatisticsTextViewComponentTests : BaseViewComponentTest
    {
        private const string _text = "Hello **strong** text";
        private const string _statisticText = "Hello **strong** statistic text";
        private const string _statisticNumber = "45%";        
        private const string _backgroundColor = "beisWhite";
        private const string _statisticNumberColor = "beisNavyBlue";
        private const string _statisticTextColor = "beisYellow";
        private const string _classNameBackgroundColor = "beis-white";
        private const string _classNameStatisticNumberColor = "beis-navy-blue-text";
        private const string _classNameStatisticTextColor = "beis-yellow-text";
        private const AlignmentValue _statisticBoxAlignment = AlignmentValue.Right;

        private MarkdownPipeline _markdownPipeline;

        [SetUp]
        public void Setup()
        {
            this._markdownPipeline = GetMarkdownPipeline();

        }

        private CmsStatisticsTextViewComponent CreateViewComponent()
        {
            return new CmsStatisticsTextViewComponent(_markdownPipeline);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Text_And_No_Statistic()
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
        public void Should_Not_Have_Content_If_No_Text()
        {
            var component = CreateViewComponent();
            var viewModel = GetValidCmsPageComponent();
            viewModel.text = null;
            var view = component.Invoke(viewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Statistic()
        {
            var component = CreateViewComponent();
            var viewModel = GetValidCmsPageComponent();
            viewModel.statisticText = null;
            var view = component.Invoke(viewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Statistic_Number()
        {
            var component = CreateViewComponent();
            var viewModel = GetValidCmsPageComponent();
            viewModel.statisticNumber = null;
            var view = component.Invoke(viewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Statistic_Alignment()
        {
            var component = CreateViewComponent();
            var viewModel = GetValidCmsPageComponent();
            viewModel.statisticBoxAlignment = null;
            var view = component.Invoke(viewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Have_Content_If_Text_Statistic_Number__And_Alignment()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
            Assert.AreEqual(model.Component.text, _text);
            Assert.AreEqual(model.Component.statisticText, _statisticText);
            Assert.AreEqual(model.Component.statisticNumber, _statisticNumber);
            Assert.AreEqual(model.Component.statisticBoxAlignment, _statisticBoxAlignment);
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

            Assert.IsNotNull(model.HtmlText);
            Assert.AreEqual(model.HtmlText, "<p>Hello <strong>strong</strong> text</p>\n");

            Assert.IsNotNull(model.HtmlStatisticText);
            Assert.AreEqual(model.HtmlStatisticText, "<p>Hello <strong>strong</strong> statistic text</p>\n");
        }

        [Test]
        public void Should_Convert_Cms_Colour_Variables_To_Valid_Class_Names()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.IsNotNull(model.Component.backgroundColor);
            Assert.AreEqual(model.Component.backgroundColor, _backgroundColor);
            Assert.IsNotNull(model.ClassNameBackgroundColour);
            Assert.AreEqual(model.ClassNameBackgroundColour, _classNameBackgroundColor);

            Assert.IsNotNull(model.Component.statisticNumberColor);
            Assert.AreEqual(model.Component.statisticNumberColor, _statisticNumberColor);
            Assert.IsNotNull(model.ClassNameStatisticNumberColour);
            Assert.AreEqual(model.ClassNameStatisticNumberColour, _classNameStatisticNumberColor);

            Assert.IsNotNull(model.Component.statisticTextColor);
            Assert.AreEqual(model.Component.statisticTextColor, _statisticTextColor);
            Assert.IsNotNull(model.ClassNameStatisticTextColor);
            Assert.AreEqual(model.ClassNameStatisticTextColor, _classNameStatisticTextColor);
        }

        [Test]
        public void Should_Handle_Cms_Null_Colour_Variables()
        {
            var component = CreateViewComponent();
            var viewModel = GetValidCmsPageComponent();
            viewModel.backgroundColor = null;
            viewModel.statisticNumberColor = null;
            viewModel.statisticTextColor = null;
            var view = component.Invoke(viewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.IsNull(model.Component.backgroundColor);
            Assert.IsNull(model.ClassNameBackgroundColour);

            Assert.IsNull(model.Component.statisticNumberColor);
            Assert.IsNull(model.ClassNameStatisticNumberColour);

            Assert.IsNull(model.Component.statisticTextColor);
            Assert.IsNull(model.ClassNameStatisticTextColor);
        }

        private static ViewDataDictionary<CmsStatisticsTextViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsStatisticsTextViewModel>;
            return viewComponentData;
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                text = _text,
                statisticText = _statisticText,
                statisticNumber = _statisticNumber,
                statisticBoxAlignment = _statisticBoxAlignment,
                backgroundColor = _backgroundColor,
                statisticNumberColor = _statisticNumberColor,
                statisticTextColor = _statisticTextColor
            };
        }
    }
}