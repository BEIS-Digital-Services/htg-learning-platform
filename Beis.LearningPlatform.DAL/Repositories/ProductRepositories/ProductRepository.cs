namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public ProductRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<product>> GetProducts()
        {
            return await _context.products.ToListAsync();
        }

        public async Task<List<product>> GetApprovedProducts()
        {
            var approvedStatusId = (await _context.product_statuses.FirstOrDefaultAsync(ps => ps.status_description.ToLower() == "approved")).id;
            if (approvedStatusId != default)
                return await _context.products.Where(p => p.status == (int)approvedStatusId).ToListAsync();
            else
                throw new Exception("There is no APPROVED status in the product_statuses table");
        }

        public async Task<List<product>> GetApprovedProductsFromApprovedVendors()
        {
            var approvedStatusId = (await _context.product_statuses.FirstOrDefaultAsync(ps => ps.status_description.ToLower() == "approved")).id;
            if (approvedStatusId == default) throw new Exception("There is no APPROVED status in the product_statuses table.");

            var approvedVendorStatusId = (await _context.vendor_statuses.FirstOrDefaultAsync(vs => vs.status_description.ToLower() == "approved")).id;
            if (approvedVendorStatusId == default) throw new Exception("There is no APPROVED status in the vendor_statuses table.");

            var approvedVendorList = (await _context.vendor_companies.Where(vc => vc.application_status == (int)approvedVendorStatusId).Distinct().Select(g => (long)g.vendorid).ToListAsync());
            return await _context.products.Where(p => p.status == (int)approvedStatusId && approvedVendorList.Contains(p.vendor_id)).ToListAsync();
        }

        public async Task<product> GetProduct(long productId)
        {
            return await _context.products.SingleOrDefaultAsync(p => p.product_id == productId);
        }

        public async Task<product> GetApprovedProductFromApprovedVendor(long productId)
        {
            const int approvedStatus = 50;
            return await _context.products.SingleOrDefaultAsync(p => p.product_id == productId && p.status == approvedStatus);
        }
    }
}