using Beis.LearningPlatform.Web.Utils;
using System;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageLink : ICmsPageLink
    {
        public int id { get; set; }
        public string text { get; set; }
        public string url { get; set; }

        public string AlteredUrl => url.StartsWith("/") ? url.Substring(1) : url.Substring(0, url.IndexOf("/", StringComparison.Ordinal) - 1);

        public string anchor { get; set; }
        public string alt { get; set; }
        public string label { get; set; }
        public string anchorlink { get; set; }
        public string custom_class { get; set; }

        public string AlteredCustomClass => CamelCaseConverter.Delimiter(custom_class, "-");

        public string aria { get; set; }
        public CMSPageIcon icon { get; set; }
        public bool? hide { get; set; }
	}
}