﻿namespace Beis.LearningPlatform.Web.Controllers
{
    public class ComparisonToolController : ControllerBase
    {
        private readonly IComparisonToolControllerHelper _comparisonToolHelper;
        private readonly IHomeControllerHelper _homeControllerHelper;

        public ComparisonToolController(ILogger<ComparisonToolController> logger, 
            IComparisonToolControllerHelper comparisonToolHelper,
            IHomeControllerHelper homeControllerHelper) : base(logger)
        {
            _comparisonToolHelper = comparisonToolHelper;
            _homeControllerHelper = homeControllerHelper;
        }


        [Route("/comparison-tool")]
        [Route("/comparison-tool/{productCategoryIds}")]
        public async Task<IActionResult> Start(string productCategoryIds = null)
        {
            return await GetStartPage(true, productCategoryIds);
        }

        [Route("/comparison-toolNoJs")]
        public async Task<IActionResult> StartNoJs()
        {
            return await GetStartPage(false);
        }


        [Route("/comparison-tool/filter-by/{productCategoryIds}")]
        [Route("/comparison-tool/filter-by")]
        public async Task<IActionResult> FilterBy(string productCategoryIds)
        {
            //If there is no category selected for filtering, redirect to the Start of Comparison tool
            if (string.IsNullOrWhiteSpace(productCategoryIds))
            {
                return RedirectToAction("Start");
            }

            var viewModel = await _comparisonToolHelper.InitViewModel("filter-by", productCategoryIds);
            await UpdateCmsPageViewModel(viewModel);
            _comparisonToolHelper.SetViewModelUserJourneyData(viewModel, productCategoryIds, null, "/");
            return View("Start", viewModel);
        }


        [Route("/comparison-tool/include-product/{productIds}")]
        [Route("/comparison-tool/include-product")]
        [Route("/comparison-tool/filter-by/{productCategoryIds}/include-product/{productIds}")]
        [Route("/comparison-tool/filter-by/{productCategoryIds}/include-product")]
        public async Task<IActionResult> IncludeProduct(string productIds, string productCategoryIds)
        {
            var viewModel = await _comparisonToolHelper.InitViewModel("include-product", productCategoryIds);
            await UpdateCmsPageViewModel(viewModel);
            _comparisonToolHelper.SetViewModelUserJourneyData(viewModel, productCategoryIds, productIds, "/");
            return View("Start", viewModel);
        }

        [Route("/comparison-tool/compare-products")]
        public async Task<IActionResult> CompareProducts(string selectedProductCategoryIds, string selectedProductIds)
        {
            return await GetCompareDetails(selectedProductCategoryIds, selectedProductIds, true);
        }

        [Route("/comparison-tool/compare-productsNoJs")]
        public async Task<IActionResult> CompareProductsNoJs(string selectedProductCategoryIds, string selectedProductIds)
        {
            return await GetCompareDetails(selectedProductCategoryIds, selectedProductIds, false);
        }

        [Route("/comparison-tool/product-details/{product_id}")]
        public async Task<IActionResult> ProductDetails(string product_id)
        {
            return await GetProductDetails(product_id, true);
        }

        [Route("/comparison-tool/product-detailsNoJs/{product_id}")]
        public async Task<IActionResult> ProductDetailsNoJs(string product_id)
        {
            return await GetProductDetails(product_id, false);
        }

        [Route("/comparison-tool/get-discount/{product_id}")]
        public async Task<IActionResult> GetDiscount(string product_id)
        {
            var voucherJourneyRedirectUrl = await _comparisonToolHelper.GetVoucherJourneyRedirectUrl(Convert.ToInt64(product_id));
            _logger.LogInformation("ComparisonTool confirmed selection: {voucherJourneyRedirectUrl}", voucherJourneyRedirectUrl);
            return Redirect(voucherJourneyRedirectUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
        }

        private async Task<ViewResult> GetStartPage(bool jsEnabled, string productCategoryIds = null)
        {
            var viewModel = await _comparisonToolHelper.InitViewModel("start", populateRelationalData: false);
            await UpdateCmsPageViewModel(viewModel);

            _comparisonToolHelper.SetViewModelUserJourneyData(viewModel, null, null, "/");
            viewModel.JavascriptEnabled = jsEnabled;
            if(!string.IsNullOrWhiteSpace(productCategoryIds))
            {
                viewModel.ProductCategoryIds = productCategoryIds.Split(',').ToArray();
            }
            return View("Start", viewModel);
        }

        private async Task<ViewResult> GetCompareDetails(string selectedProductCategoryIds, string selectedProductIds, bool jsEnabled)
        {
            var viewModel = await _comparisonToolHelper.InitViewModel("compare-products", selectedProductCategoryIds, selectedProductIds);
            await UpdateCmsPageViewModel(viewModel);
            _comparisonToolHelper.SetViewModelUserJourneyData(viewModel, selectedProductCategoryIds, selectedProductIds, "/comparison-tool");

            UpdateViewModel(jsEnabled, viewModel);
            if (jsEnabled)
            {
                TempData["SelectedProductIds"] = selectedProductIds;
            }

            return View("CompareProducts", viewModel);
        }

        private async Task<ActionResult> GetProductDetails(string productId, bool jsEnabled)
        {
            var viewModel = await _comparisonToolHelper.InitViewModelForSelectedProduct(Convert.ToInt64(productId));
            if (viewModel == null)
            {
                return NotFound();
            }

            await UpdateCmsPageViewModel(viewModel, "comparison-tool-product");

            UpdateViewModel(jsEnabled, viewModel);
            viewModel.pageTitle = "Help to Grow: Digital - More about this software";
            return View("ProductDetails", viewModel);
        }

        private static void UpdateViewModel(bool jsEnabled, ComparisonToolPageViewModel model)
        {
            model.JavascriptEnabled = jsEnabled;
            model.Referrer = jsEnabled ? model.Referrer : $"{model.Referrer}NoJs";
        }

        private async Task UpdateCmsPageViewModel(ComparisonToolPageViewModel model, string customPage = "comparison-tool")
        {
            var cmsPageViewModel = await _homeControllerHelper.ProcessGetCustomPageResult($"Custom-pages/{customPage}");
            model.CMSPageViewModel = cmsPageViewModel;
        }
    }
}