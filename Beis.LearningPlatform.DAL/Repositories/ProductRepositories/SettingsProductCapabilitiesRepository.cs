namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories
{
    public class SettingsProductCapabilitiesRepository : ISettingsProductCapabilitiesRepository

    {
        private readonly HtgVendorSmeDbContext _context;

        public SettingsProductCapabilitiesRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<settings_product_capability>> GetSettingsProductCapabilities()
        {
            return await _context.settings_product_capabilities.ToListAsync();
        }
    }
}