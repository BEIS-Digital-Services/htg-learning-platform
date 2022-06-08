namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSSearchTag
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string action { get; set; }
        public DateTime published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string displayName { get; set; }
        public IList<CMSPageIcon> icon { get; set; }
        public bool? hide { get; set; }
    }
}