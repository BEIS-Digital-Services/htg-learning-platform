using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}