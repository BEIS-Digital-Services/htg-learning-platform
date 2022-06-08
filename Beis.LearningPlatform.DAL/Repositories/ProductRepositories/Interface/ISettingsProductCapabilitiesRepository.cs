namespace Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface
{
    public interface ISettingsProductCapabilitiesRepository
    {
        Task<List<settings_product_capability>> GetSettingsProductCapabilities();
    }
}