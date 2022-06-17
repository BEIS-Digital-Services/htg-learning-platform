
namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface
{
    public interface IProductRepository
    {
        Task<List<product>> GetProducts();

        Task<List<product>> GetApprovedProducts();

        Task<List<product>> GetApprovedProductsFromApprovedVendors();

        Task<product> GetApprovedProductFromApprovedVendor(long productId);
    }
}