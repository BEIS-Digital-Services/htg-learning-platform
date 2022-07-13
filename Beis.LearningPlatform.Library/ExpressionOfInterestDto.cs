using System.ComponentModel.DataAnnotations;

namespace Beis.LearningPlatform.Library
{
    public class ExpressionOfInterestDto
    {
        [Required]
        public string PageName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        public string UserBusinessName { get; set; }

        [Required]
        public string UserPhone { get; set; }
        public bool OptInMarketingEmail { get; set; }
        public bool OptInMarketingPhone { get; set; }
        public bool OptInReadPrivacy { get; set; }
        public DateTime RecordCreatedUtc { get; set; }
    }
}