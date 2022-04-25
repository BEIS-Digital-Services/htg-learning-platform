using Beis.LearningPlatform.Library.DTO.Base;

namespace Beis.LearningPlatform.Library
{
    /// <summary>
    /// A class that defines a DTO for a Skills results email data.
    /// </summary>
    public class SkillsResultsEmailDataDto : DtoBase, IEmailDto
    {
        // Question 1
        public string DigitalAdoptionBenefits { get; set; }

        // Question 2
        public string DigitalAdoptionFrictionPointDescription { get; set; }

        // Question 3
        public string SoftwareUsage { get; set; }

        // Question 4
        public string InformationSharingMode { get; set; }

        // Question 5
        public string DigitalAdoptionBenefitsDescription { get; set; }
    }
}