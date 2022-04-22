using System.ComponentModel.DataAnnotations;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSFeedbackProblem
    {
        public string sessionId { get; set; }
        public string url { get; set; }
        [Required]
        public string whatIWasDoing { get; set; }
        [Required]
        public string whatWentWrong { get; set; }
    }
}
