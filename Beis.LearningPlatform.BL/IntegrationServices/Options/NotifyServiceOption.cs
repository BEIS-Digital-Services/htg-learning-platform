using System.Collections.Generic;

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
        public Mover Mover { get; set; }
        public Newcomer Newcomer { get; set; }
        public Performer Performer { get; set; }
    }

    public class SkilledModuleThreeBase
    {
        public string Communication { get; set; }
        public string Planning { get; set; }
        public string Maintenance { get; set; }
        public string Testing { get; set; }
        public string Training { get; set; }
    }

    public class Mover : SkilledModuleThreeBase
    {
    }

    public class Newcomer : SkilledModuleThreeBase
    {
    }

    public class Performer : SkilledModuleThreeBase
    {
    }

}