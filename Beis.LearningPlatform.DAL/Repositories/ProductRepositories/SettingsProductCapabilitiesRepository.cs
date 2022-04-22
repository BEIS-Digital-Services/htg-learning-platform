using Beis.Htg.VendorSme.Database;
using Beis.Htg.VendorSme.Database.Models;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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