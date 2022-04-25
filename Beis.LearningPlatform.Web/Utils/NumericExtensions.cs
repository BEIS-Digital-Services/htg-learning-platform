using System.Globalization;

namespace Beis.LearningPlatform.Web.Utils
{
    public static class NumericExtensions
    {
        private static readonly CultureInfo _cultureInfo = new("en-GB");

        /// <remarks>
        /// Copied from code in razor views.
        /// </remarks>
        public static string ToCurrencyFormat(this decimal number)
        {
            return number.ToString("C2", _cultureInfo);
        }
    }
}