using Beis.LearningPlatform.Web.Utils;
using NUnit.Framework;

namespace Beis.LearningPlatform.Web.Tests.ServicesTests
{
    public class StrapiMakeApiCallServiceTests
    {
        [Test]
        public void ListJoinFormatter_ReplaceLastCommaWith_ReturnsOK()
        {
            string input = "A, B, C";
            var result = ListJoinFormatter.ReplaceLastCommaWith(input, "and");
            Assert.NotNull(result);
        }


        [Test]
        public void ListJoinFormatter_ReplaceLastCharacterWith_ReturnsOK()
        {
            string input = "A, B, C";
            var result = ListJoinFormatter.ReplaceLastCharacterWith(input, ",", "and");
            Assert.NotNull(result);
        }

        [Test]
        public void CamelCaseConverter_Delimiter_ReturnsOK()
        {
            string input = "camelCaseConverter";
            var result = CamelCaseConverter.Delimiter(input, "-");
            Assert.NotNull(result);
        }

        [Test]
        public void CamelCaseConverter_Delimiter_Null_Input_ReturnsNull()
        {
            var result = CamelCaseConverter.Delimiter(null, "-");
            Assert.IsNull(result);
        }
    }
}