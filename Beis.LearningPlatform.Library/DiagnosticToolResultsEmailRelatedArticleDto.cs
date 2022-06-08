namespace Beis.LearningPlatform.Library
{
    /// <summary>
    /// A class that defines a DTO for a Diagnostic Tool results email related article.
    /// </summary>
    public class DiagnosticToolResultsEmailRelatedArticleDto : DtoBase
    {
        /// <summary>
        /// Gets or sets the article's URL.
        /// </summary>
        public string ArticleURL { get; set; }

        /// <summary>
        /// Gets or sets the description of the article.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the title of the article.
        /// </summary>
        public string Title { get; set; }
    }
}