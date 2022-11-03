using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Data.Entities.Skills
{
    [ExcludeFromCodeCoverage]
    public class SkilledDataResponse : FeedbackEntity
    {
        public bool? IsPrivacyPolicyAccepted { get; set; }
        public bool? IsOptedInForMarketing { get; set; }
    }
}