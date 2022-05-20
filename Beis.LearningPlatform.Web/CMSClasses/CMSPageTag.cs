using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageTag
    {
        public int id { get; set; }
        public string label { get; set; }
        public string url { get; set; }
        public string anchor { get; set; }
        public string type { get; set; }
        public string customClass { get; set; }
        public string anchorlink { get; set; }
        public string custom_class { get; set; }

        public string AlteredCustomClass => $"govuk-button govuk-button--start {(string.IsNullOrWhiteSpace(custom_class) ? "tag-link" : custom_class)}";

        public IList<CMSPageIcon> icon { get; set; }
        public string name { get; set; }
        public string action { get; set; }
        public string displayName { get; set; }

        public bool? hide { get; set; }
    }
}