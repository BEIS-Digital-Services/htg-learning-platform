namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageArticles
    {
        public string __component { get; set; }
        public int id { get; set; }
        public string Type { get; set; }
        public string type { get; set; }
        public CMSPageColumn Column1 { get; set; }
        public CMSPageColumn Column2 { get; set; }
        public CMSPageColumn Column3 { get; set; }
        public IList<CMSPageLink> Links1 { get; set; }
        public IList<CMSPageLink> Links2 { get; set; }
        public IList<CMSPageLink> Links3 { get; set; }
        public IList<CMSPageTag> Tags1 { get; set; }
        public IList<CMSPageTag> Tags2 { get; set; }
        public IList<CMSPageTag> Tags3 { get; set; }
        public CMSPageImage Image1 { get; set; }
        public CMSPageImage Image2 { get; set; }
        public CMSPageImage Image3 { get; set; }
    }
}