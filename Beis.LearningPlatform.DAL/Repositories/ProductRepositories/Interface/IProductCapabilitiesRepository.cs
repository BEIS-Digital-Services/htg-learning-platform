using Beis.Htg.VendorSme.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface
{
    public interface IProductCapabilitiesRepository
    {
        Task<List<product_capability>> GetProductCapabilitiesFilters(long productId = 0);
    }
}