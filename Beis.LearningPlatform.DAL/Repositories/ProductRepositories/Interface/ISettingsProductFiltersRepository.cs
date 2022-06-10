namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface
{
    public interface ISettingsProductFiltersRepository
    {
        Task<List<settings_product_filter>> GetSettingsProductFilters(long filterType);
    }
}