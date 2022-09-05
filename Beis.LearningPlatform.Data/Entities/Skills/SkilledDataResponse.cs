namespace Beis.LearningPlatform.Data.Entities.Skills
{
    public class SkilledDataResponse : FeedbackEntity
    {
        public bool? IsPrivacyPolicyAccepted { get; set; }
        public bool? IsOptedInForMarketing { get; set; }
    }
}