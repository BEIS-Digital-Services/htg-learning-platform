namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface
{
    public interface IProductFiltersRepository
    {
        Task<List<product_filter>> GetProductFilters(long productId);
    }
}