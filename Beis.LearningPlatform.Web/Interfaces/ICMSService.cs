namespace Beis.LearningPlatform.Web.Interfaces
{
    public interface ICmsService
    {
        public Task<CMSPageViewModel> GetCustomPageResult(string strapiAction);        

        public Task<IList<CMSSearchArticle>> GetSearchArticlesResult(string strapiAction);
        
        public Task<IList<CMSSearchTag>> GetSearchTagsResult(string strapiAction);

        public Task<CMSPageViewModel> FilterCustomPageResultByTags(IList<string> currentTagNames);
        
        public Task<IList<ComparisonToolPageViewModel>> GetComparisonToolPageResult(string strapiAction);

        Task<IEnumerable<SiteNavigationModel>> GetSiteNavigation();
        Task<IEnumerable<GlobalWarningMessageModel>> GetGlobalWarningMessages(bool enabledOnly = true);

        /// <param name="orderByIds">Return in same order as Id params</param>
        Task<IEnumerable<CMSSearchArticle>> GetSearchArticles(IEnumerable<int> searchArticleIds, bool orderByIds = false);
        Task<IList<CMSComparisonToolSearchTag>> GetDisplaySettings();
    }
}