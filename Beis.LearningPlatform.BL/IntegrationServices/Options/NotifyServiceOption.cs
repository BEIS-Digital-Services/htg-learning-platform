namespace Beis.LearningPlatform.BL.IntegrationServices.Options
{
    public class NotifyServiceOption
    {
        public const string NotifyService = "IntegrationServicesConfig:NotifyServiceConfig";

        public string ApiKey { get; set; }

        public string BaseUrl { get; set; }

        public Templates Templates { get; set; }

        public string UnsubscribeLink { get; set; }

        public string SkilledModule2Link { get; set; }

        public string SkilledModule3Link { get; set; }
    }

    public class Templates
    {
        public string DtResultPageQ6Else { get; set; }

        public string DtResultPageQ6No { get; set; }

        public string DtResultPageQ6Yes { get; set; }

        public string SkillsModule1 { get; set; }

        public string SkillsModule2DigitalMover { get; set; }

        public string SkillsModule2DigitalNewcomer { get; set; }

        public string SkillsModule2DigitalPerformer { get; set; }

        public SkillsModuleThree SkillsModuleThree { get; set; }
    }

    public class SkillsModuleThree
    {
        public string MoverCommunication { get; set; }
        public string MoverPlanning { get; set; }
        public string MoverSupport { get; set; }
        public string MoverTesting { get; set; }
        public string MoverTraining { get; set; }

        public string NewcomerCommunication { get; set; }
        public string NewcomerPlanning { get; set; }
        public string NewcomerSupport { get; set; }
        public string NewcomerTesting { get; set; }
        public string NewcomerTraining { get; set; }

        public string PerformerCommunication { get; set; }
        public string PerformerPlanning { get; set; }
        public string PerformerSupport { get; set; }
        public string PerformerTesting { get; set; }
        public string PerformerTraining { get; set; }
    }

}