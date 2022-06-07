namespace Beis.LearningPlatform.Web.Services
{
    public class HtmlTextService : IHtmlTextService
    {
        /// <summary>
        /// u2000 = EN QUAD
        /// u2001 = EM QUAD
        /// u2003 = EM SPACE
        /// u202F = NARROW NO-BREAK SPACE
        /// u0009 = <control> HORIZONTAL TAB
        /// u000a = <control> LINE FEED
        /// u000b = <control> VERTICAL TAB
        /// u000c = <contorl> FORM FEED
        /// u000d = <control> CARRIAGE RETURN
        /// u0085 = <control> NEXT LINE
        /// u00a0 = NO-BREAK SPACE
        /// </summary>
        private static readonly Regex _rxWhiteSpace = new("[\u202F\u2000\u2001\u2003\u0009\u000a\u000b\u000c\u000d\u0085\u00a0]", RegexOptions.Compiled);

        public string ReplaceLineBreaks(string input, string replacement = "<br />")
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            return input.Replace(Environment.NewLine, replacement);
        }

        public string CleanWhiteSpace(string input, string replacement = " ")
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            return _rxWhiteSpace.Replace(input, replacement);
        }
    }
}