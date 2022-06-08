namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories
{

    public class ProductCapabilitiesRepository : IProductCapabilitiesRepository

    {
        private readonly HtgVendorSmeDbContext _context;

        public ProductCapabilitiesRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<product_capability>> GetProductCapabilitiesFilters(long productId = 0)
        {
            if (productId > 0)
            {
                return await _context.product_capabilities.Where(x => x.product_id == productId).ToListAsync();
            }

            return await _context.product_capabilities.ToListAsync();
        }

    }
}