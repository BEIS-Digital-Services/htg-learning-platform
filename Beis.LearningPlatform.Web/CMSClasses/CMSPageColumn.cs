using Beis.LearningPlatform.Web.Utils;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageColumn
    {
        public int id { get; set; }
        public string subheader { get; set; }
        public string copy { get; set; }
        public string header { get; set; }
        public string seubheaderColor { get; set; }
        public string backgroundColor { get; set; }

        public string AlteredBackgroundColor => !string.IsNullOrWhiteSpace(backgroundColor) ? CamelCaseConverter.Delimiter(backgroundColor, "-") : string.Empty;

        public string subheaderColor { get; set; }
        public string subHeaderColor { get; set; }
    }
}