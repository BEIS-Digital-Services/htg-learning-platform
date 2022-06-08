namespace Beis.LearningPlatform.Web.Models
{
    public class SatisfactionSurveyViewModel
    {
        public string Url { get; set; }

        [Required(ErrorMessage = "Please tell us how you feel about the service you received today")]
        public string Rating { get; set; }

        [MaxLength(1200, ErrorMessage = "Please limit your comments to 1200 characters.")]
        public string Comment { get; set; }

        public IList<string> RatingOptions => new List<string>
        {
            { "Very satisfied" },
            { "Satisfied" },
            { "Neither satisfied or dissatisfied" },
            { "Dissatisfied" },
            { "Very dissatisfied" }
        };
    }
}