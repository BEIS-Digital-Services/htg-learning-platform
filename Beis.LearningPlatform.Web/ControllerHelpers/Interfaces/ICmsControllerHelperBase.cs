using Beis.LearningPlatform.Web.StrapiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface ICmsControllerHelperBase : IControllerHelperBase
    {
        Task<CMSPageViewModel> ProcessFilterCustomPageResultByTags(IList<string> currentTags);

        Task<CMSPageViewModel> ProcessFilterCustomPageResultByTags(IList<string> currentTags, string cmsDisplayName);
        
        Task<CMSPageViewModel> ProcessGetCustomPageResult(string strapiAction);
        
        Task<IList<CMSSearchArticle>> ProcessGetSearchArticlesResult(string strapiAction);            
    }
}