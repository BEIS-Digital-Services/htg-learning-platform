namespace Beis.LearningPlatform.Web.Tests.ServicesTests
{
    public class HtmlTextServiceTests
    {
        private const string _dirtyWhiteSpaceText = @"Gold-Vision Service Professional embeds our ""Simple by Design"" ethos within a highly intelligent system. Your service team can manage customer queries efficiently with all the tools needed for the job within a 360° customer view";
        private const string _cleanWhiteSpaceText = @"Gold-Vision Service Professional embeds our ""Simple by Design"" ethos within a highly intelligent system. Your service team can manage customer queries efficiently with all the tools needed for the job within a 360° customer view";

        private const string _lineBreakText = @"This package also gives you access to our full B2B CRM suite, including modules such as sales, marketing, and projects.

Gold-Vision lets you keep on top of customer issues ";
        private const string _convertedLineBreakText = @"This package also gives you access to our full B2B CRM suite, including modules such as sales, marketing, and projects.<br /><br />Gold-Vision lets you keep on top of customer issues ";
        private IHtmlTextService _htmlTextService;


        [SetUp]
        public void Setup()
        {
            this._htmlTextService = new HtmlTextService();
        }

        [Test]
        public void Should_Clean_White_Space()
        {
            var cleanText = _htmlTextService.CleanWhiteSpace(_dirtyWhiteSpaceText, " ");
            Assert.AreEqual(cleanText, _cleanWhiteSpaceText);
        }

        [Test]
        public void Should_Handle_Null_For_Clean_White_Space()
        {
            var cleanText = _htmlTextService.CleanWhiteSpace(null, " ");
            Assert.AreEqual(cleanText, null);

            cleanText = _htmlTextService.CleanWhiteSpace(string.Empty, " ");
            Assert.AreEqual(cleanText, string.Empty);
        }


        [Test]
        public void Should_Replace_Line_Breaks()
        {
            var cleanText = _htmlTextService.ReplaceLineBreaks(_lineBreakText, "<br />");
            Assert.AreEqual(cleanText, _convertedLineBreakText);
        }

        [Test]
        public void Should_Handle_Null_For_Replace_Line_Breaks()
        {
            var cleanText = _htmlTextService.ReplaceLineBreaks(null, "<br />");
            Assert.AreEqual(cleanText, null);

            cleanText = _htmlTextService.ReplaceLineBreaks(string.Empty, "<br />");
            Assert.AreEqual(cleanText, string.Empty);
        }
    }
}