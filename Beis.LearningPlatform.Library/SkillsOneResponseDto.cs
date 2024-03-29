﻿using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library
{
    [ExcludeFromCodeCoverage(Justification = "Basic DTO - only data fields, no functions")]
    public class SkillsOneResponseDto : DtoBase
    {
        public bool? IsPrivacyPolicyAccepted { get; set; }
        public bool? IsOptedInForMarketing { get; set; }

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