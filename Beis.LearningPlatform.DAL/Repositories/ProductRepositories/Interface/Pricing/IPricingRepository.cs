using Beis.Htg.VendorSme.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Pricing
{
    public interface IPricingRepository
    {
        Task<List<product_price>> GetAllProductPricesForProductId(long productId);
        Task<List<product_price_base_description>> GetAllProductPriceBaseDescriptions();
        Task<List<product_price_base_metric_price>> GetAllProductBaseMetricPricesByProductPriceId(long productPriceId);
        Task<List<product_price_secondary_description>> GetAllProductPriceSecondaryDescriptions();
        Task<List<product_price_secondary_metric>> GetAllProductSecondaryMetricPricesByProductPriceId(long productPriceId);
        Task<List<additional_cost>> GetAdditionalCostsByProductPriceId(long productPriceId);
        Task<List<additional_cost_desc>> GetAllAdditionalCostDescriptions();
        Task<List<user_discount>> GetAllUserDiscountsByProductPriceId(long productPriceId);
    }
}