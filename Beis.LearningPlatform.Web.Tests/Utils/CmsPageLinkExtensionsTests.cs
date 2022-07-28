namespace Beis.LearningPlatform.Web.Tests.Utils
{

    [TestFixture]
    public class CmsPageLinkExtensionsTests
    {
        [TestCase(null, null, "prefix-10")]
        [TestCase(" ", null, "prefix-10")]
        [TestCase("", "", "prefix-10")]
        [TestCase("Label", "", "prefix-label")]
        [TestCase(" Label ", "", "prefix-label")]
        [TestCase("Label", "/some-url", "prefix-label-some-url")]
        [TestCase(" Label ", "some-url", "prefix-labelsome-url")]
        public void Should_GetGaLinkId(string inputLabel, string url, string expected)
        {
            // Arrange
            var model = new CMSPageLink { id = 10, label = inputLabel, url = url };

            // Act
            var result = model.GetGaLinkId("prefix-");

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