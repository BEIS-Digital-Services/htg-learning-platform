namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageFooter
    {
        public int id { get; set; }
        public string copyright { get; set; }
        public string copy { get; set; }
        public string name { get; set; }
        public DateTime published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public IList<CMSPageLink> links { get; set; }
    }
}