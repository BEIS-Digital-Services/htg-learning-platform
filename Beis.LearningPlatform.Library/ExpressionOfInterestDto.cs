using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library
{
    public class ExpressionOfInterestDto
    {
        [Required]
        [ExcludeFromCodeCoverage]
        public string PageName { get; set; }

        [Required]
        [ExcludeFromCodeCoverage]
        public string UserName { get; set; }

        [Required]
        [ExcludeFromCodeCoverage]
        public string UserBusinessName { get; set; }

        [ExcludeFromCodeCoverage]
        public string UserEmail { get; set; }

        [ExcludeFromCodeCoverage]
        public string UserPhone { get; set; }

        [ExcludeFromCodeCoverage]
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

        [ExcludeFromCodeCoverage]
        public DateTime RecordCreatedUtc { get; set; }
    }
}