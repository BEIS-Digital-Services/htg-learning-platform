namespace Beis.LearningPlatform.Web.Services
{
    public class ComparisonToolService : IComparisonToolService
    {
        private readonly IMapper _mapper;
        private readonly ICmsService _cmsService;

        // VENDOR Product Repository
        private readonly IProductRepository _productRepository;
        private readonly IPricingRepository _pricingRepository;
        private readonly ISettingsProductCapabilitiesRepository _capabilitiesRepository;
        private readonly IProductCapabilitiesRepository _productCapabilitiesRepository;
        private readonly ISettingsProductFiltersRepository _filtersRepository;
        private readonly IProductFiltersRepository _productFiltersRepository;

        public ComparisonToolService(
                                    IMapper mapper,
                                    ICmsService cmsService,
                                    IProductRepository productRepository,
                                    IProductCapabilitiesRepository productCapabilitiesRepository,
                                    IPricingRepository pricingRepository,
                                    ISettingsProductCapabilitiesRepository capabilitiesRepository,
                                    ISettingsProductFiltersRepository filtersRepository,
                                    IProductFiltersRepository productFiltersRepository)
        {
            _mapper = mapper;
            _cmsService = cmsService;
            _productRepository = productRepository;
            _productCapabilitiesRepository = productCapabilitiesRepository;
            _pricingRepository = pricingRepository;
            _capabilitiesRepository = capabilitiesRepository;
            _filtersRepository = filtersRepository;
            _productFiltersRepository = productFiltersRepository;
        }

        public async Task<IList<ComparisonToolProduct>> GetProducts()
        {
            var products = (await _productRepository.GetProducts()).ToArray();
            var productsVm = await TransformProductModel(products);
            return productsVm.ToList();
        }

        public async Task<IList<ComparisonToolProduct>> GetApprovedProductsFromApprovedVendors()
        {
            var products = (await _productRepository.GetApprovedProductsFromApprovedVendors()).ToArray();
            var productsVm = await TransformProductModel(products);
            return productsVm.ToList();
        }

        public async Task<ComparisonToolProduct> GetApprovedProductFromApprovedVendor(long productId)
        {
            var product = await _productRepository.GetApprovedProductFromApprovedVendor(productId);
            return await TransformProductModel(product);
        }

        public async Task PopulateChildRelationships(IList<ComparisonToolProduct> products)
        {
            foreach (var item in products)
            {
                await Populate(item);
            }
        }

        public async Task<bool> SetNavAndFooter(ComparisonToolPageViewModel comparisonToolPage)
        {
            var strapiAction = "Comparison-tools";
            var comparisonToolStrapiPage = await _cmsService.GetComparisonToolPageResult(strapiAction);
            comparisonToolPage.navigations = comparisonToolStrapiPage.FirstOrDefault()?.navigations;
            comparisonToolPage.footers = comparisonToolStrapiPage.FirstOrDefault()?.footers;
            return await Task.FromResult(true);
        }

        private async Task Populate(ComparisonToolProduct product)
        {
            product.settingsProductCapabilities = (await _capabilitiesRepository.GetSettingsProductCapabilities()).Where(x => x.product_type == product.product_type).ToList();
            product.productCapabilities = await _productCapabilitiesRepository.GetProductCapabilitiesFilters(product.product_id);
            product.productFilters = await _productFiltersRepository.GetProductFilters(product.product_id);

            product.settingsProductTrainingFilters = (await _filtersRepository.GetSettingsProductFilters(2)).ToList();
            product.settingsProductSupportFilters = (await _filtersRepository.GetSettingsProductFilters(1)).ToList();
            product.settingsProductPltatformCompatibilityFilters = (await _filtersRepository.GetSettingsProductFilters(3)).ToList();

            var trainingList = product.settingsProductTrainingFilters.Select(item => item.filter_id);
            var supportList = product.settingsProductSupportFilters.Select(item => item.filter_id);
            var compatibilityList = product.settingsProductPltatformCompatibilityFilters.Select(item => item.filter_id);


            product.productTrainingFilters = product.productFilters.Where(item => trainingList.Contains(item.filter_id)).ToList();
            product.productSupportFilters = product.productFilters.Where(item => supportList.Contains(item.filter_id)).ToList();
            product.productPltatformCompatibilityFilters = product.productFilters.Where(item => compatibilityList.Contains(item.filter_id)).ToList();

            product.productPriceSecondaryDescriptions = await _pricingRepository.GetAllProductPriceSecondaryDescriptions();
            product.productPriceAddCostDescriptions = await _pricingRepository.GetAllAdditionalCostDescriptions();

            var productPriceId = product.productPrices.Count > 0 ? product.productPrices.FirstOrDefault()?.product_price_id : null;
            if (productPriceId.HasValue)
            {
                product.productUserDiscount =
                    (await _pricingRepository.GetAllUserDiscountsByProductPriceId(productPriceId.Value))
                    .OrderBy(x => x.min_licenses).ToList();
                product.productPriceSecondaryMetrics =
                    await _pricingRepository.GetAllProductSecondaryMetricPricesByProductPriceId(productPriceId.Value);

                var productPriceAdditionalCosts = await _pricingRepository.GetAdditionalCostsByProductPriceId(productPriceId.Value);
                if (productPriceAdditionalCosts.Any())
                {
                    product.productPriceAddCosts = productPriceAdditionalCosts
                        .Where(x => x.additional_cost_type_id == (int)EnumAdditionalCostType.General)
                        .Select(x => new ComparisonToolAdditionalCost(x)).ToList();
                    product.productPriceThirdPartyFees = productPriceAdditionalCosts
                        .Where(x => x.additional_cost_type_id == (int)EnumAdditionalCostType.ThirdPartyFee)
                         .Select(x => new ComparisonToolAdditionalCost(x)).ToList();
                    product.productPriceTransactionFees = productPriceAdditionalCosts
                        .Where(x => x.additional_cost_type_id == (int)EnumAdditionalCostType.TransactionFee)
                        .Select(x => new ComparisonToolAdditionalCost(x)).ToList();
                }
            }
        }
        
        private async Task<ComparisonToolProduct[]> TransformProductModel(product[] products)
        {
            var comparisonToolProducts = _mapper.Map<ComparisonToolProduct[]>(products);
            foreach (var comparisonToolProduct in comparisonToolProducts)
            {
                await SetInternals(comparisonToolProduct);
            }
            return comparisonToolProducts;
        }

        private async Task<ComparisonToolProduct> TransformProductModel(product product)
        {
            var comparisonToolProduct = _mapper.Map<ComparisonToolProduct>(product);
            return await SetInternals(comparisonToolProduct);
        }

        private async Task<ComparisonToolProduct> SetInternals(ComparisonToolProduct comparisonToolProduct)
        {
            comparisonToolProduct.productPrices = (await _pricingRepository.GetAllProductPricesForProductId(comparisonToolProduct.product_id)).Where(x => x.productid == comparisonToolProduct.product_id).ToList();
            comparisonToolProduct.productPriceBaseMetricDescription = await _pricingRepository.GetAllProductPriceBaseDescriptions();

            var productPriceId = comparisonToolProduct.productPrices.Count > 0 ? comparisonToolProduct.productPrices.FirstOrDefault()?.product_price_id : null;
            if (productPriceId.HasValue)
            {
                comparisonToolProduct.productPriceBaseMetricPrice = await _pricingRepository.GetAllProductBaseMetricPricesByProductPriceId(productPriceId.Value);
            }

            return comparisonToolProduct;
        }
    }
}