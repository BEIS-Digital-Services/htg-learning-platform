using Beis.Htg.VendorSme.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface
{
    public interface IProductFiltersRepository
    {
        Task<List<product_filter>> GetProductFilters(long productId);
    }
}