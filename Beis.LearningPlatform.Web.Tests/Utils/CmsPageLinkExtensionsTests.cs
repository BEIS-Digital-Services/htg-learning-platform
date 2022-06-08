namespace Beis.LearningPlatform.Web.Tests.Utils
{

    [TestFixture]
    public class CmsPageLinkExtensionsTests
    {
        [TestCase(null, "prefix10")]
        [TestCase(" ", "prefix10")]
        [TestCase("", "prefix10")]
        [TestCase("Label", "prefixlabel-10")]
        [TestCase(" Label ", "prefix-label-10")]
        public void Should_GetGaLinkId(string inputLabel, string expected)
        {
            // Arrange
            var model = new CMSPageLink { id = 10, label = inputLabel };

            // Act
            var result = model.GetGaLinkId("prefix");

            // Assert
            result.Should().Be(expected);
        }

        [TestCase("/TestUrl")]
        [TestCase("TestUrl/")]
        public void Should_GetCmsLinkUrl(string inputUrl)
        {
            // Arrange
            var model = new CMSPageLink { url = inputUrl };

            // Act
            var result = model.GetCmsLinkUrl();

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().NotContain("/");
        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        [TestCase("abc")]
        public void Should_GetCustomClass(string inputCustomClass)
        {
            // Arrange
            var model = new CMSPageLink { custom_class = inputCustomClass };

            // Act
            var result = model.GetCustomClass();

            // Assert
            result.Should().Be(inputCustomClass);
        }
    }
}