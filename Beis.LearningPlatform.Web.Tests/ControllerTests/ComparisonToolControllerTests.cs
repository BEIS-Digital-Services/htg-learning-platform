using Beis.LearningPlatform.Web.Configuration;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class ComparisonToolControllerTests
    {
        private readonly Mock<ILogger<ComparisonToolController>> _controllerLogger = new();
        private readonly Mock<IHomeControllerHelper> _homeControllerHelper = new();
        private readonly Mock<ILogger<ComparisonToolControllerHelper>> _helperLogger = new();
        private readonly Mock<IComparisonToolService> _comparisonToolService = new();
        private readonly Mock<ICmsService> _cmsService = new();
        private IOptions<VoucherAppOption> _voucherAppOptions;
        private IOptions<VendorAppOption> _vendorAppOptions;
        private IOptions<ComparisonToolDisplayOption> _ctDisplayOptions;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
        private readonly Mock<HttpContext> _httpContext = new();
        private readonly Mock<HttpRequest> _httpRequest = new();
        private IProductCategoryDisplaySettings _productCategoryDisplaySettings;

        #region Test Values 

        private const string TestValueRequestReferer = "_requestReferer";
        private static readonly IList<ComparisonToolProduct> TestValueComparisonToolProducts = new List<ComparisonToolProduct>()
        {
            new() { product_type = 2, product_id = 4, product_name = "product-4", status = 1,
            },
            new() { product_type = 1, product_id = 7, product_name = "product-7", status = 1,
                productPriceAddCosts = GetAddCostTestData(),
            },
            new() { product_type = 2, product_id = 10, product_name = "product-10", status = 50,
                productPriceAddCosts = GetAddCostTestData(),
                productPriceThirdPartyFees = GetAddCostTestData() 
            },
            new() { product_type = 1, product_id = 14, product_name = "product-14", status = 50,
                productPriceAddCosts = GetAddCostTestData(),
                productPriceTransactionFees = GetAddCostTestData(),
            },
        };

        private static IList<ComparisonToolAdditionalCost> GetAddCostTestData()
        {
            return new List<ComparisonToolAdditionalCost> { 
                new ComparisonToolAdditionalCost{ 
                     CostDescription = "AdditionalCost_Desc1",
                     CostAndFrequency = "AdditionalCost_CostFreq1",
                     Mandatory = true
                },
                new ComparisonToolAdditionalCost{ 
                     CostDescription = "AdditionalCost_Desc2",
                     CostAndFrequency = "AdditionalCost_CostFreq2",
                     Mandatory = true
                }
            };
        }

        private static IEnumerable<object[]> CompareProductInput =>
            new List<object[]>
            {
                new object[] { null, "4,10" },
                new object[] { null, "7,14" },
                new object[] { null, "4,7,10,14" },
                new object[] { "accounting", "7" },
                new object[] { "accounting", "14" },
                new object[] { "accounting", "7,14" },
                new object[] { "crm", "4" },
                new object[] { "crm", "10" },
                new object[] { "crm", "4,10" },
                new object[] { "crm,accounting", "4,7,10,14" }
            };

        private static IEnumerable<object[]> TestInput =>
            new List<object[]>
            {
                new object[] { "4" },
                new object[] { "7" },
                new object[] { "10" },
                new object[] { "14" }
            };

        private static IEnumerable<object[]> TestInputApproved =>
            new List<object[]>
            {
                new object[] { "10" },
                new object[] { "14" }
            };

        private static IEnumerable<object[]> TestInputNotApproved =>
            new List<object[]>
            {
                new object[] { "4" },
                new object[] { "7" }
            };
        #endregion


        private ComparisonToolController CreateController(IOptions<ComparisonToolDisplayOption> ctDisplayOptions = null)
        {
            var productCategoryDisplaySettings = ctDisplayOptions == null ? _productCategoryDisplaySettings : new ProductCategoryDisplaySettings(ctDisplayOptions);
            var controlHelper = new ComparisonToolControllerHelper(_helperLogger.Object,
                _comparisonToolService.Object,
                _voucherAppOptions,
                _vendorAppOptions,
                _ctDisplayOptions,
                _httpContextAccessor.Object,
                _cmsService.Object);
            return new ComparisonToolController(_controllerLogger.Object, controlHelper, _homeControllerHelper.Object);
        }

        [SetUp]
        public void Setup()
        {
            _voucherAppOptions = ConfigOptions.Create(new VoucherAppOption { BaseUrl = "https://www.voucherapp.com" });
            _vendorAppOptions = ConfigOptions.Create(new VendorAppOption { ProdLogoUrl = "https://www.vendorapp.com/logo" });
            _ctDisplayOptions = ConfigOptions.Create(new ComparisonToolDisplayOption());

            _httpRequest.SetupGet(x => x.Headers)
                .Returns(new HeaderDictionary { { "Referer", TestValueRequestReferer } });
            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _productCategoryDisplaySettings = new ProductCategoryDisplaySettings(_ctDisplayOptions);
            _comparisonToolService.Setup(x => x.GetApprovedProductsFromApprovedVendors())
                .Returns(Task.FromResult(TestValueComparisonToolProducts));
        }

        [Test]
        public async Task Should_Return_Valid_Start_View()
        {
            var controller = CreateController();

            var viewResult = await controller.Start() as ViewResult;

            AssertStart(viewResult, true);
        }

        [Test]
        public async Task Should_Return_Valid_StartNoJs_View()
        {
            var controller = CreateController();

            var viewResult = await controller.StartNoJs() as ViewResult;
            
            AssertStart(viewResult, false);
        }

        [Test]
        public async Task Should_Redirect_To_Start_If_Filter_By_Null()
        {
            var controller = CreateController();

            var result = await controller.FilterBy(null);
            Assert.IsNotNull(result);
            Assert.That(result is RedirectToActionResult);

            var viewResult = result as RedirectToActionResult;
            Assert.AreEqual("Start", viewResult.ActionName);
        }

        [TestCase("crm")]
        [TestCase("accounting")]
        [TestCase("crm,accounting")]
        [Test]
        public async Task Should_Return_Products_Matching_Filter_By_Values(string productCategoryNames)
        {
            var controller = CreateController();

            var result = await controller.FilterBy(productCategoryNames);
            Assert.IsNotNull(result);
            Assert.That(result is ViewResult);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.Model);

            var viewModel = viewResult.Model as ComparisonToolPageViewModel;
            Assert.IsNotNull(viewModel);
            Assert.IsNotNull(viewModel.products);
            Assert.IsTrue(viewModel.products.Any());


            var productCategoryIdList = productCategoryNames.Split(',').Select(categoryName =>
                _productCategoryDisplaySettings.DisplaySettings.FirstOrDefault(_ => _.name == categoryName).id.ToString()).ToList();

            var productTypeList = viewModel.products.Select(x => x.product_type.ToString()).ToList();

            Assert.IsTrue(productCategoryIdList.Intersect(productTypeList).Count() == productCategoryIdList.Count);
            Assert.IsFalse(productCategoryIdList.Except(productTypeList).Any());

            Assert.AreEqual("Start", viewResult.ViewName);
        }

        [TestCase("7", "accounting")]
        [TestCase("14", "accounting")]
        [TestCase("7,14", "accounting")]
        [TestCase("4", "crm")]
        [TestCase("10", "crm")]
        [TestCase("4,10", "crm")]
        [TestCase("4,7,10,14", "crm,accounting")]
        [TestCase("4,7,10,14", null)]
        [TestCase("4,10", null)]
        [TestCase("7,14", null)]
        [Test]
        public async Task Should_Return_IncludeProduct_Matching_Category_And_Product_Ids(string productIds, string productCategoryIds)
        {
            var controller = CreateController();

            var result = await controller.IncludeProduct(productIds, productCategoryIds);
            Assert.IsNotNull(result);
            Assert.That(result is ViewResult);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.Model);

            var viewModel = viewResult.Model as ComparisonToolPageViewModel;
            Assert.IsNotNull(viewModel);
            Assert.IsNotNull(viewModel.products);
            Assert.IsTrue(viewModel.products.Any());

            //Check productIds are correctly filtered:
            var productIdList = productIds.Split(',').Select(_ => int.Parse(_)).ToList();
            var returnedProductIds = viewModel.products.Select(x => (int)x.product_id).ToList();

            Assert.IsTrue(productIdList.Intersect(returnedProductIds).Count() == productIdList.Count);
            Assert.IsFalse(productIdList.Except(returnedProductIds).Any());

            Assert.AreEqual("Start", viewResult.ViewName);
        }


        [TestCase("7", "crm")] // Unmatching products
        [TestCase("14", "crm")]
        [TestCase("7,14", "crm")]
        [TestCase("4", "accounting")]
        [TestCase("10", "accounting")]
        [TestCase("4,10", "accounting")]
        [Test]
        public async Task Should_Return_No_IncludeProduct_When_Category_Not_Matching_Product_Ids(string productIds, string productCategoryIds)
        {
            var controller = CreateController();

            var result = await controller.IncludeProduct(productIds, productCategoryIds);
            Assert.IsNotNull(result);
            Assert.That(result is ViewResult);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.Model);

            var viewModel = viewResult.Model as ComparisonToolPageViewModel;
            Assert.IsNotNull(viewModel);
            Assert.IsNotNull(viewModel.products);
            Assert.IsTrue(viewModel.products.Any());

            var productIdList = productIds.Split(',').Select(_ => int.Parse(_)).ToList();
            var returnedProductIds = viewModel.products.Select(x => (int)x.product_id).ToList();

            // Test input does not match (products are not in those categories) - so we should have no intersecting - and all should be excluded:
            Assert.IsFalse(productIdList.Intersect(returnedProductIds).Any());
            Assert.IsTrue(productIdList.Except(returnedProductIds).Count() == productIdList.Count);

            Assert.AreEqual("Start", viewResult.ViewName);
        }

        [TestCaseSource(nameof(CompareProductInput))]
        public async Task Should_Return_CompareProducts_Matching_Category_And_Product_Ids(string productCategoryIds, string productIds)
        {
            var controller = CreateController();
            var tempData = new Mock<ITempDataDictionary>();
            controller.TempData = tempData.Object;

            var viewResult = await controller.CompareProducts(productCategoryIds, productIds) as ViewResult;

            AssertCompareProducts(viewResult, productIds, true);
        }

        [TestCaseSource(nameof(CompareProductInput))]
        public async Task Should_Return_CompareProductsNoJs_Matching_Category_And_Product_Ids(string productCategoryIds, string productIds)
        {
            var controller = CreateController();

            var viewResult = await controller.CompareProductsNoJs(productCategoryIds, productIds) as ViewResult;

            AssertCompareProducts(viewResult, productIds, false);
        }

        [TestCaseSource(nameof(TestInput))]
        public async Task Should_Return_ProductDetails_Matching_Product_Ids(string productId)
        {
            var controller = CreateController();
            _comparisonToolService.Setup(x => x.GetApprovedProductFromApprovedVendor(Convert.ToInt64(productId)))
                .ReturnsAsync(TestValueComparisonToolProducts.First(r => r.product_id == Convert.ToInt64(productId)));

            var viewResult = await controller.ProductDetails(productId) as ViewResult;

            AssertProductDetails(viewResult, productId, true);
        }

        [TestCaseSource(nameof(TestInputApproved))]
        public async Task Should_Return_Approved_ProductDetails_Matching_Product_Ids(string productId)
        {
            var controller = CreateController();
            _comparisonToolService.Setup(x => x.GetApprovedProductFromApprovedVendor(Convert.ToInt64(productId)))
                .ReturnsAsync(TestValueComparisonToolProducts.First(r => r.product_id == Convert.ToInt64(productId) && r.status == 50));

            var viewResult = await controller.ProductDetails(productId) as ViewResult;

            AssertProductDetails(viewResult, productId, true);
        }

        [TestCaseSource(nameof(TestInputNotApproved))]
        public async Task Should_Not_Return_UnApproved_ProductDetails_Matching_Product_Ids(string productId)
        {
            var controller = CreateController();
            _comparisonToolService.Setup(x => x.GetApprovedProductFromApprovedVendor(Convert.ToInt64(productId)))
                .ReturnsAsync(TestValueComparisonToolProducts.FirstOrDefault(r => r.product_id == Convert.ToInt64(productId) && r.status == 50));

            var actionResult = await controller.ProductDetails(productId) as ActionResult;
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [TestCaseSource(nameof(TestInputNotApproved))]
        public async Task Should_Return_UnApproved_ProductDetails_If_Show_All_Statuses_Enabled(string productId)
        {
            var ctDisplayOptions = ConfigOptions.Create(new ComparisonToolDisplayOption() {
                ShowAllProductStatuses = true
            });
            var controller = CreateController(ctDisplayOptions);

            _comparisonToolService.Setup(x => x.GetProduct(Convert.ToInt64(productId)))
                .ReturnsAsync(TestValueComparisonToolProducts.FirstOrDefault(r => r.product_id == Convert.ToInt64(productId)));            

            var viewResult = await controller.ProductDetails(productId) as ViewResult;

            AssertProductDetails(viewResult, productId, true);
        }

        [TestCaseSource(nameof(TestInput))]
        public async Task Should_Return_ProductDetailsNoJs_Matching_Product_Ids(string productId)
        {
            var controller = CreateController();
            _comparisonToolService.Setup(x => x.GetApprovedProductFromApprovedVendor(Convert.ToInt64(productId)))
                .ReturnsAsync(TestValueComparisonToolProducts.Single(r => r.product_id == Convert.ToInt64(productId)));

            var viewResult = await controller.ProductDetailsNoJs(productId) as ViewResult;

            AssertProductDetails(viewResult, productId, false);
        }

        [TestCaseSource(nameof(TestInput))]
        public async Task Should_Return_GetDiscount_Redirect_Url_Matching_Product_Id(string productId)
        {
            var controller = CreateController();
            
            var result = await controller.GetDiscount(productId) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Url);
            var selectedProduct = TestValueComparisonToolProducts.Single(x => x.product_id == Convert.ToInt64(productId));
            var selectionQueryString = $"?product_id={selectedProduct.product_id}&product_type={selectedProduct.product_type}";
            Assert.IsTrue(result.Url.EndsWith(selectionQueryString));
        }

        [TestCaseSource(nameof(CompareProductInput))]
        public async Task Should_Have_Additional_Cost_Data(string productCategoryIds, string productIds)
        {
            var controller = CreateController();
            var tempData = new Mock<ITempDataDictionary>();
            controller.TempData = tempData.Object;

            var viewResult = await controller.CompareProducts(productCategoryIds, productIds) as ViewResult;

            AssertCompareProducts(viewResult, productIds, true);

            var viewModel = viewResult.Model as ComparisonToolPageViewModel;
            Assert.IsNotNull(viewModel);

            var productIdsParsed = productIds.Split(',').Select(x => long.Parse(x)).ToArray();
            var testDataProducts = TestValueComparisonToolProducts.Where(x => productIdsParsed.Contains(x.product_id));

            Assert.AreEqual(testDataProducts.Any(x => x.productPriceTransactionFees?.Any() ?? false), viewModel.AnyProductHasTransactionFees);
            Assert.AreEqual(testDataProducts.Any(x => x.productPriceThirdPartyFees?.Any() ?? false), viewModel.AnyProductHasThirdPartyFees);

            Assert.AreEqual(testDataProducts.Any(x => x.productPriceTransactionFees?.Any() ?? false), viewModel.ComparisonTransactionFeeDescriptions?.Any() ?? false);
            Assert.AreEqual(testDataProducts.Any(x => x.productPriceThirdPartyFees?.Any() ?? false), viewModel.ComparisonThirdPartyFeeDescriptions?.Any() ?? false);
        }

        [Test]
        public void Should_Return_ErrorViewModel_From_Error()
        {
            var controller = CreateController();

            var result = controller.Error();
            Assert.IsNotNull(result);
            Assert.That(result is ViewResult);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.Model);

            Assert.IsTrue(viewResult.Model is ErrorViewModel);            
        }

        private static void AssertStart(ViewResult viewResult, bool expectedJavascriptEnabled)
        {
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("Start", viewResult.ViewName);
            var model = viewResult.Model as ComparisonToolPageViewModel;
            Assert.NotNull(model);
            Assert.IsNotEmpty(model.ContentKey);
            Assert.AreEqual(expectedJavascriptEnabled, model.JavascriptEnabled);
        }

        private static void AssertCompareProducts(ViewResult viewResult, string productIds, bool expectedJavascriptEnabled)
        {
            Assert.IsNotNull(viewResult);
            var viewModel = viewResult.Model as ComparisonToolPageViewModel;
            Assert.IsNotNull(viewModel);
            Assert.IsNotNull(viewModel.products);
            Assert.IsTrue(viewModel.products.Any());

            //Check productIds are correctly filtered:
            var productIdList = productIds.Split(',').Select(_ => int.Parse(_)).ToList();
            var returnedProductIds = viewModel.products.Select(x => (int)x.product_id).ToList();
            Assert.IsTrue(productIdList.Intersect(returnedProductIds).Count() == productIdList.Count);
            Assert.IsFalse(productIdList.Except(returnedProductIds).Any());
            Assert.AreEqual("CompareProducts", viewResult.ViewName);
            Assert.IsNotEmpty(viewModel.ContentKey);
            Assert.AreEqual(expectedJavascriptEnabled, viewModel.JavascriptEnabled);
        }

        private static void AssertProductDetails(ViewResult viewResult, string productId, bool expectedJavascriptEnabled)
        {
            Assert.IsNotNull(viewResult);
            var viewModel = viewResult.Model as ComparisonToolPageViewModel;
            Assert.IsNotNull(viewModel);
            Assert.IsNotNull(viewModel.products);
            Assert.IsTrue(viewModel.products.Any());
            Assert.IsTrue(viewModel.products.Count == 1);
            Assert.IsNotEmpty(viewModel.ContentKey);

            //Check productIds are correctly filtered:
            var returnedProduct = viewModel.products.SingleOrDefault();
            Assert.IsNotNull(returnedProduct);
            Assert.AreEqual(long.Parse(productId), returnedProduct.product_id);
            Assert.AreEqual(viewModel.ContentKey, $"comparison-tool-product-details-{returnedProduct.product_name}");
            Assert.AreEqual("ProductDetails", viewResult.ViewName);
            Assert.AreEqual(expectedJavascriptEnabled, viewModel.JavascriptEnabled);
        }
    }
}