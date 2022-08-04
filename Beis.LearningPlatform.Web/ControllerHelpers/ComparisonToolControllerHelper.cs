namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class ComparisonToolControllerHelper : ControllerHelperBase, IComparisonToolControllerHelper
    {
        private readonly IComparisonToolService _comparisonToolService;
        private readonly VoucherAppOption _voucherAppOption;
        private readonly VendorAppOption _vendorAppOption;
        private readonly ComparisonToolDisplayOption _ctDisplayOption;
        private readonly ICmsService _cmsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ComparisonToolControllerHelper(ILogger<ComparisonToolControllerHelper> logger
            , IComparisonToolService comparisonToolService
            , IOptions<VoucherAppOption> voucherAppOption
            , IOptions<VendorAppOption> vendorAppOption
            , IOptions<ComparisonToolDisplayOption> ctDisplayOptions
            , IHttpContextAccessor httpContextAccessor
            , ICmsService cmsService
            ) : base(logger)
        {
            _comparisonToolService = comparisonToolService;
            _voucherAppOption = voucherAppOption.Value;
            _vendorAppOption = vendorAppOption.Value;
            _ctDisplayOption = ctDisplayOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            _cmsService = cmsService;
        }

        public async Task<IList<ComparisonToolProduct>> ProcessGetProductList(string productCategoryIds)
        {
            var displaySettings = await _cmsService.GetDisplaySettings();
            var distinctProductCategoryIds = displaySettings.Distinct().Select(g => (long)g.id);
            var existingCategoriesList = !string.IsNullOrWhiteSpace(productCategoryIds) ?
                displaySettings.Where(pc => productCategoryIds.Split(",").ToList().Contains(pc.systemName)).Select(g => (long)g.id) : distinctProductCategoryIds;

            if (_ctDisplayOption.ShowAllProductStatuses ?? false)
            {
                var productsVm = await _comparisonToolService.GetProducts();
                return productsVm.Where(p => existingCategoriesList.Contains(p.product_type)).ToList();
            }
            else
            {
                var productsVm = await _comparisonToolService.GetApprovedProductsFromApprovedVendors();
                return productsVm.Where(p => existingCategoriesList.Contains(p.product_type)).ToList();
            }
        }

        public async Task<ComparisonToolPageViewModel> InitViewModel(string contentKey, string productCategoryIds = null, string productIds = null, bool populateRelationalData = true)
        {
            var viewModel = new ComparisonToolPageViewModel
            {
                Referrer = _httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString(),
                ContentKey = $"comparison-tool-{contentKey}"
            };

            await _comparisonToolService.SetNavAndFooter(viewModel);

            var products = await this.ProcessGetProductList(productCategoryIds);

            var currentProductIds = productIds?.Split(",");
            if (currentProductIds != null)
            {
                products = products.Where(product => currentProductIds.Contains(product.product_id.ToString()))
                    .ToList();
            }

            viewModel.products = products.RandomOrder().ToList();

            if (populateRelationalData)
            {
                await _comparisonToolService.PopulateChildRelationships(viewModel.products);
            }

            return viewModel;
        }

        public async Task<ComparisonToolPageViewModel> InitViewModelForSelectedProduct(long productId)
        {
            var viewModel = new ComparisonToolPageViewModel
            {
                Referrer = _httpContextAccessor.HttpContext?.Request.Headers["Referer"].ToString()
            };

            await _comparisonToolService.SetNavAndFooter(viewModel);

            var showAllProductStatuses = _ctDisplayOption.ShowAllProductStatuses ?? false;
            var currentProduct = showAllProductStatuses ? 
                                    await _comparisonToolService.GetProduct(productId) :
                                        await _comparisonToolService.GetApprovedProductFromApprovedVendor(productId);

            if (currentProduct == null)
            {
                return null;
            }

            viewModel.ContentKey = $"comparison-tool-product-details-{currentProduct?.product_name.UrlEncode(true)}";
            viewModel.products = new List<ComparisonToolProduct> { currentProduct };
            await _comparisonToolService.PopulateChildRelationships(viewModel.products);
            this.SetViewModelUserJourneyData(viewModel, null, null, "/comparison-tool");

            viewModel.currentProduct = currentProduct;

            return viewModel;
        }

        public void SetViewModelUserJourneyData(ComparisonToolPageViewModel viewModel, string productCategoryIds, string productIds, string referrerPath)
        {
            var displaySettings = _cmsService.GetDisplaySettings().Result;
            viewModel.VoucherUrl = _voucherAppOption.BaseUrl;
            viewModel.VendorProdLogorUrl = _vendorAppOption.ProdLogoUrl;
            viewModel.ProductCategoryList = displaySettings;

            if (string.IsNullOrWhiteSpace(productCategoryIds))
            {
                foreach (var item in viewModel.ProductCategoryList.ToList())
                {
                    // If there are no products exist for any of the tags, then remove it from filter option.
                    if (!viewModel.products.Any(p => p.product_type == item.id))
                    {
                        viewModel.ProductCategoryList.Remove(item);
                    }
                }
            }

            viewModel.Referrer = referrerPath;

            // If there are one or more category selected, apply the filter and set the ViewModel attribute
            if (!string.IsNullOrWhiteSpace(productCategoryIds))
            {
                //Apply the filter if applicable
                var existingCategories = productCategoryIds.Split(",").ToList();
                if (existingCategories.Count > 0)
                {
                    viewModel.products = viewModel.products.Where(x => existingCategories.Contains(displaySettings.First(c => c.id == x.product_type).systemName)).ToList();
                }
            }

            viewModel.SelectedProductCategoryId = productCategoryIds;
            viewModel.SelectedProductId = productIds;
        }

        public async Task<string> GetVoucherJourneyRedirectUrl(long productId)
        {
            var products = await ProcessGetProductList(null);
            var selectedProduct = products.SingleOrDefault(x => x.product_id == productId);
            return $"{_voucherAppOption.BaseUrl}/?product_id={selectedProduct?.product_id}&product_type={selectedProduct?.product_type}";
        }
    }
}