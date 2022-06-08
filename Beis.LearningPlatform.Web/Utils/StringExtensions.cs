using System.Text;

namespace Beis.LearningPlatform.Web.Utils
{
    /// <summary>
    /// A class that defines extensions to a string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Joins an array of strings into a single, separated list of strings.
        /// </summary>
        /// <param name="items">An array of string that are the items to join.</param>
        /// <param name="separator">A string containing the separator to use to separate the elements.</param>
        /// <param name="finalSeparator">A string containing the separator to use between the last two elements.</param>
        /// <returns>A string containing the list of strings.</returns>
        public static string JoinToSeparatedList(this string[] items, string separator = ", ", string finalSeparator = " and ")
        {
            StringBuilder returnValue = new();

            if (items != default)
            {
                int length = items.Length;
                int finalSeparatorIndex = length > 1 ? length - 2 : 0;
                int finalIndex = length - 1;

                for (int i = 0; i < length; i++)
                {
                    returnValue.Append(items[i].Trim());

                    if (length > 1 && i == finalSeparatorIndex)
                        returnValue.Append(finalSeparator);
                    else if (i < finalIndex)
                        returnValue.Append(separator);
                }
            }
            else
                throw new ArgumentNullException(nameof(items));

            return returnValue.ToString();
        }

        public static string UrlEncode(this string str, bool lower)
        {
            if (string.IsNullOrEmpty(str)) 
            {
                return str;
            }

            var urlEncoded = HttpUtility.UrlEncode(str);
            return lower ? urlEncoded.ToLowerInvariant() : urlEncoded;
        }
    }
}
