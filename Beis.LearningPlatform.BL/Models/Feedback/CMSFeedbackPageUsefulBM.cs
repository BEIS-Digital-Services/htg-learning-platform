using System.ComponentModel.DataAnnotations;

namespace Beis.LearningPlatform.BL.Models
{
    public class CMSFeedbackPageUsefulBM
    {
        public string sessionId { get; set; }
        public string url { get; set; }
        [Required]
        public string IsPageUseful { get; set; }
    }
}