namespace Beis.LearningPlatform.Library
{
    /// <summary>
    /// A class that defines a DTO for a Skills modeul three results email data.
    /// </summary>
    public class SkilledModuleThreeDto : DtoBase, IEmailDto
    {
        public string QuestionOneStart { get; set; }
        public string QuestionOneNext { get; set; }
        public string QuestionOneFinally { get; set; }

        public string QuestionTwoStart { get; set; }
        public string QuestionTwoNext { get; set; }
        public string QuestionTwoFinally { get; set; }

        public string QuestionThreeStart { get; set; }
        public string QuestionThreeNext { get; set; }
        public string QuestionThreeFinally { get; set; }

        public string UserTypeActionPlanSection { get; set; }
    }
}