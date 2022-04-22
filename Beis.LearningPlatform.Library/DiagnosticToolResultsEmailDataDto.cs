using Beis.LearningPlatform.Library.DTO.Base;

namespace Beis.LearningPlatform.Library
{
    /// <summary>
    /// A class that defines a DTO for a Diagnostic Tool results email data.
    /// </summary>
    public class DiagnosticToolResultsEmailDataDto : DtoBase, IEmailDto
    {
        /// <summary>
        /// Gets or sets the business sector.
        /// </summary>
        public string BusinessSector { get; set; }

        /// <summary>
        /// Gets or sets the answer to question 6.
        /// </summary>
        public string Question6Answer { get; set; }

        /// <summary>
        /// Gets or sets the related articles.
        /// </summary>
        public DiagnosticToolResultsEmailRelatedArticleDto[] RelatedArticles { get; set; }

        /// <summary>
        /// Gets or sets the recommended software.
        /// </summary>
        public string RecommendedSoftware { get; set; }

        /// <summary>
        /// Gets or sets the software choices.
        /// </summary>
        public string SoftwareChoices { get; set; }

        /// <summary>
        /// Gets or sets the software needed.
        /// </summary>
        public string SoftwareNeed { get; set; }

        /// <summary>
        /// Gets or sets the streamline tasks.
        /// </summary>
        public string StreamlineTasks { get; set; }
        
        /// <summary>
        /// Gets or sets the business' trade method.
        /// </summary>
        public string TradeMethod { get; set; }
    }
}