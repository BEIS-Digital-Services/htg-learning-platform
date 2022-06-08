using Beis.LearningPlatform.Web.Utils;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageTextblock
    {
        public int id { get; set; }
        public string subheader { get; set; }
        public string copy { get; set; }
        public string header { get; set; }
        public string subheaderColor { get; set; }

        public string AlteredSubHeaderColor => !string.IsNullOrWhiteSpace(subheaderColor) ? CamelCaseConverter.Delimiter(subheaderColor, "-") : string.Empty;

        public string backgroundColor { get; set; }
        public string subHeaderColor { get; set; }
        public bool? hide { get; set; }
        public string headerAlign { get; set; }
        public string subHeaderAlign { get; set; }
        public string contentAlign { get; set; }
        public string copyAlign { get; set; }
        public CMSPageImage HeaderImage { get; set; }

    }
}