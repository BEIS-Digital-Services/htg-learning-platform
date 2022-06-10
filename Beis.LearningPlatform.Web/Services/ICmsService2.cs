namespace Beis.LearningPlatform.Web.Services
{
    /// <summary>
    /// An interface that defines CMS services for the Web application.
    /// </summary>
    public interface ICmsService2
    {
        /// <summary>
        /// Gets the page from the CMS with the specified reference.
        /// </summary>
        /// <param name="pageReference">A string containing the page reference.</param>
        /// <returns>A Task representing the asynchronous operation.  A CMSPageViewModel containing the view model for the page.</returns>
        Task<CMSPageViewModel> GetPage(string pageReference);

        /// <summary>
        /// Gets search articles from the CMS.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.  An array of CMSSearchArticle containing the search result.</returns>
        Task<CMSSearchArticle[]> GetSearchArticles();
        /// <summary>
        /// Gets search articles from the CMS.
        /// </summary>
        /// <param name="searchTags">An IEnumerable of type string containing the search tags to use.</param>
        /// <returns>A Task representing the asynchronous operation.  An array of CMSSearchArticle containing the search result.</returns>
        Task<CMSSearchArticle[]> GetSearchArticles(IEnumerable<string> searchTags);
    }
}