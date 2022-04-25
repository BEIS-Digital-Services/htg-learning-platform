namespace Beis.LearningPlatform.BL.Models
{
    /// <summary>
    /// A class that defines a data for a Diagnostic Tool results email.
    /// </summary>
    public class DiagnosticToolResultsEmailData : IEmailDataModel
    {
        /// <summary>
        /// Gets or sets the business sector.
        /// </summary>
        public string BusinessSector { get; set; }

        /// <summary>
        /// Gets or sets the answer to question 6.
        /// </summary>
        public DiagnosticToolQuestion6Type Question6Answer { get; set; }

        /// <summary>
        /// Gets or sets the related articles.
        /// </summary>
        public DiagnosticToolResultsEmailRelatedArticle[] RelatedArticles { get; set; }

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