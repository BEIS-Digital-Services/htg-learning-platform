namespace Beis.LearningPlatform.Web.DependencyInjection
{
    /// <summary>
    /// A class that defines a profile for an Comparison Tool Product View Model.
    /// </summary>
    public class ComparisonToolProductProfile : Profile
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public ComparisonToolProductProfile()
        {
            CreateMap<product, ComparisonToolProduct>()
                .ForMember(dest => dest.productPrices, opt => opt.Ignore())
                .ForMember(dest => dest.settingsProductCapabilities, opt => opt.Ignore())
                .ForMember(dest => dest.productCapabilities, opt => opt.Ignore())
                .ForMember(dest => dest.settingsProductTrainingFilters, opt => opt.Ignore())
                .ForMember(dest => dest.settingsProductSupportFilters, opt => opt.Ignore())
                .ForMember(dest => dest.settingsProductPltatformCompatibilityFilters, opt => opt.Ignore())
                .ForMember(dest => dest.productFilters, opt => opt.Ignore())
                .ForMember(dest => dest.productTrainingFilters, opt => opt.Ignore())
                .ForMember(dest => dest.productSupportFilters, opt => opt.Ignore())
                .ForMember(dest => dest.productPltatformCompatibilityFilters, opt => opt.Ignore())
                .ForMember(dest => dest.productPriceBaseMetricPrice, opt => opt.Ignore())
                .ForMember(dest => dest.productPriceBaseMetricDescription, opt => opt.Ignore())
                .ForMember(dest => dest.productUserDiscount, opt => opt.Ignore())
                .ForMember(dest => dest.productPriceSecondaryDescriptions, opt => opt.Ignore())
                .ForMember(dest => dest.productPriceSecondaryMetrics, opt => opt.Ignore())
                .ForMember(dest => dest.productPriceAddCostDescriptions, opt => opt.Ignore())
                .ForMember(dest => dest.productPriceAddCosts, opt => opt.Ignore());
        }
    }
}