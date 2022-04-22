using Beis.LearningPlatform.Web.ComparisonTool.Models;
using Beis.LearningPlatform.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface IComparisonToolControllerHelper
    {
        Task<ComparisonToolPageViewModel> InitViewModel(string contentKey, string productCategoryIds = null, string productIds = null, bool populateRelationalData = true);

        Task<ComparisonToolPageViewModel> InitViewModelForSelectedProduct(long productId);

        Task<IList<ComparisonToolProduct>> ProcessGetProductList(string productCategoryIds);

        void SetViewModelUserJourneyData(ComparisonToolPageViewModel viewModel, string productCategoryIds, string productIds, string referrerPath);
        
        Task<string> GetVoucherJourneyRedirectUrl(long productId);
    }
}