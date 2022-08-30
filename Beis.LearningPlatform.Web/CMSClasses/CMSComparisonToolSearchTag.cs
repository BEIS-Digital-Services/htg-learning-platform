namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSComparisonToolSearchTag
    {
        public int id { get; set; }
        public int systemId { get; set; }
        public string systemName { get; set; }
        public string displayName { get; set; }
        public string friendlyDisplayName { get; set; }
        public bool isActive { get; set; }
        public int sortOrder { get; set; }
        public DateTime published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        
    }
}