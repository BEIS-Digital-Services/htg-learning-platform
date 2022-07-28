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
        public string UserBusinessName { get; set; }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        public bool OptInReadPrivacy { get; set; }

        public bool OptInMarketingEmail
        {
            get
            {
                return !string.IsNullOrEmpty(UserEmail);
            }
        }

        public bool OptInMarketingPhone {
            get
            {
                return !string.IsNullOrEmpty(UserPhone);
            }
        }

        public DateTime RecordCreatedUtc { get; set; }
    }
}