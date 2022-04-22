using Beis.Htg.VendorSme.Database;
using Beis.Htg.VendorSme.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Pricing
{
    public class PricingRepository : IPricingRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public PricingRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<product_price>> GetAllProductPricesForProductId(long productId)
        {
            return await _context.product_prices.Where(x => x.productid == productId).ToListAsync();
        }

        public async Task<List<product_price_base_description>> GetAllProductPriceBaseDescriptions()
        {
            return await _context.product_price_base_descriptions.ToListAsync();
        }

        public async Task<List<product_price_base_metric_price>> GetAllProductBaseMetricPricesByProductPriceId(long productPriceId)
        {
            return await _context.product_price_base_metric_prices.Where(x => x.product_price_id == productPriceId).ToListAsync(); ;
        }

        public async Task<List<product_price_secondary_description>> GetAllProductPriceSecondaryDescriptions()
        {
            return await _context.product_price_secondary_descriptions.ToListAsync();
        }

        public async Task<List<product_price_secondary_metric>> GetAllProductSecondaryMetricPricesByProductPriceId(long productPriceId)
        {
            return await _context.product_price_secondary_metrics.Where(x => x.product_price_id == productPriceId).ToListAsync(); ;
        }

        public async Task<List<additional_cost>> GetAdditionalCostsByProductPriceId(long productPriceId)
        {
            return await _context.additional_costs.Where(x => x.product_price_id == productPriceId).ToListAsync();
        }

        public async Task<List<additional_cost_desc>> GetAllAdditionalCostDescriptions()
        {
            return await _context.additional_cost_descs.ToListAsync();
        }

        public async Task<List<user_discount>> GetAllUserDiscountsByProductPriceId(long productPriceId)
        {
            return await _context.user_discounts.Where(x => x.product_price_id == productPriceId).ToListAsync();
        }
    }
}