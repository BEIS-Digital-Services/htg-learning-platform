using System.ComponentModel.DataAnnotations;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSFeedbackPageUseful
    {
        public string sessionId { get; set; }
        public string url { get; set; }
        [Required]
        public string IsPageUseful { get; set; }
    }
}