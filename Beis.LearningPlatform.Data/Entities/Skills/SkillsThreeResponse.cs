namespace Beis.LearningPlatform.Data.Entities.Skills
{
    public class SkillsThreeResponse : FeedbackEntity
    {
        public string UserEmailAddress { get; set; }
        public string Questionnaire { get; set; }
        public string WhyNeedStart { get; set; }
        public string WhyNeedNext { get; set; }
        public string WhyNeedFinally { get; set; }
        public string HowAccessStart { get; set; }
        public string HowAccessNext { get; set; }
        public string HowAccessFinally { get; set; }
        public string RiskStart { get; set; }
        public string RiskNext { get; set; }
        public string RiskFinally { get; set; }
    }
}