using Beis.LearningPlatform.Data.Entities.Base;

namespace Beis.LearningPlatform.Data.Entities.Skills
{
    public class SkillsOneResponse : FeedbackEntity
    {
        public string UserEmailAddress { get; set; }
        public string InterestedInNewOpportunities { get; set; }
        public string InterestedInIncreasingSales { get; set; }
        public string InterestedInAutomatingTasks { get; set; }
        public string InterestedInRealTimeData { get; set; }
        public string BiggestFriction { get; set; }
        public string HowMuchSoftwareUsed { get; set; }
        public string ShareInfoInPerson { get; set; }
        public string ShareInfoSharedDatabase { get; set; }
        public string ShareInfoMeetings { get; set; }
        public string ShareInfoInformationConversations { get; set; }
        public string ShareInfoSomethingElse { get; set; }
        public string ShareInfoAdditionalInfo { get; set; }
        public string DigitalAdoption { get; set; }
    }
}