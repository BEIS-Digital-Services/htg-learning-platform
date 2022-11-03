using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Data.Entities.Skills
{
    [ExcludeFromCodeCoverage]
    public class SkillsTwoResponse : SkilledDataResponse
    {
        public string UserEmailAddress { get; set; }
        public string UseTechnologyForCommunication { get; set; }
        public string ImproveCommunication { get; set; }
        public string UseCloudStorage { get; set; }
        public string ImproveDataStorage { get; set; }
        public string UseOnlinePayments { get; set; }
        public string ImprovePayments { get; set; }
        public string UseOnlineAdvertise { get; set; }
        public string ImproveOnlineMarketing { get; set; }
        public string UseOnlineShop { get; set; }
        public string ImproveOnlineBusiness { get; set; }
        public string UsePersonaliseMessagesToCustomers { get; set; }
        public string ImprovePersonalisedMarketing { get; set; }
        public string UseSoftwareForPlanning { get; set; }
        public string ImprovePlanning { get; set; }
        public string UseDigitalTraining { get; set; }
        public string ImproveDigitalTraining { get; set; }
        public string UseSoftwareForInformationSharing { get; set; }
        public string ImproveInformationSharing { get; set; }
    }
}