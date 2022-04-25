namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageButton
    {
        public int id { get; set; }
        public string label { get; set; }
        public string url { get; set; }
        public string anchor { get; set; }
        public string type { get; set; }
        public string anchorlink { get; set; }
        public string custom_class { get; set; }
        public CMSPageIcon icon { get; set; }
    }
}