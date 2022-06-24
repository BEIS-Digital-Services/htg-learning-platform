namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageLink : ICmsPageLink
    {
        public int id { get; set; }
        public string text { get; set; }
        public string url { get; set; }

        public string AlteredUrl => this.GetCmsLinkUrl();

        public string anchor { get; set; }
        public string alt { get; set; }
        public string label { get; set; }
        public string anchorlink { get; set; }
        public string custom_class { get; set; }

        public string AlteredCustomClass => this.GetCustomClass();

        public string aria { get; set; }
        public CMSPageIcon icon { get; set; }
        public bool? hide { get; set; }
	}
}