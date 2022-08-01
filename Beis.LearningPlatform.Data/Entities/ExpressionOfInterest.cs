namespace Beis.LearningPlatform.Data.Entities
{
    public class ExpressionOfInterest : Entity
    {
        public string PageName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserBusinessName { get; set; }
        public string UserPhone { get; set; }
        public bool OptInMarketingEmail { get; set; }
        public bool OptInMarketingPhone { get; set; }
        public bool OptInReadPrivacy { get; set; }
        public DateTime RecordCreatedUtc { get; set; }
    }
}