namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories
{
    public class ProductFiltersRepository : IProductFiltersRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public ProductFiltersRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<product_filter>> GetProductFilters(long productId)
        {
            return await _context.product_filters.Where(pf => pf.product_id == productId).ToListAsync();
        }
    }
}