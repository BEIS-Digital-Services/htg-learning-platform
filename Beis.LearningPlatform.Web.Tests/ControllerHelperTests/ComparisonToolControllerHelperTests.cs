using Beis.LearningPlatform.Web.Configuration;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests;

public class ComparisonToolControllerHelperTests
{
    private ComparisonToolControllerHelper _controllerHelper;
    private Mock<ILogger<ComparisonToolControllerHelper>> _logger;
    private Mock<IComparisonToolService> _comparisonToolService;
    private IOptions<VoucherAppOption> _voucherOption;
    private IOptions<VendorAppOption> _vendorOption;
    private ProductCategoryDisplaySettings _productCategoryDisplaySettings;
    private Mock<IHttpContextAccessor> _httpContextAccessor;
    private IOptions<ComparisonToolDisplayOption> _displayOption;


    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<ComparisonToolControllerHelper>>();
        _comparisonToolService = new Mock<IComparisonToolService>();
        _voucherOption = ConfigOptions.Create(new VoucherAppOption());
        _vendorOption = ConfigOptions.Create(new VendorAppOption());
        _displayOption = ConfigOptions.Create(new ComparisonToolDisplayOption());
        _productCategoryDisplaySettings =
            new ProductCategoryDisplaySettings(_displayOption);
        _httpContextAccessor = new Mock<IHttpContextAccessor>();

        _httpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());

        _comparisonToolService
            .Setup(x => x.GetApprovedProductsFromApprovedVendors())
            .ReturnsAsync(new List<ComparisonToolProduct>()
            {
                new ComparisonToolProduct()
                {
                    product_id = 0,
                    product_type = 1,
                    product_name = "test"
                }
            });

        _controllerHelper = new ComparisonToolControllerHelper(
            _logger.Object,
            _comparisonToolService.Object,
            _voucherOption,
            _vendorOption,
            _productCategoryDisplaySettings,
            _httpContextAccessor.Object
            );
    }

    [Test]
    public async Task Should_get_valid_redirect_url()
    {
        _voucherOption.Value.BaseUrl = "test";
        var result = await _controllerHelper.GetVoucherJourneyRedirectUrl(0);
        result.Should().NotBeNull();
        result.Should().Be("test/?product_id=0&product_type=1");
    }

    [Test]
    public async Task Should_get_valid_product_list_all()
    {
        var result = await _controllerHelper.ProcessGetProductList(string.Empty);
        result.Should().NotBeNull();
        result.Count().Should().BePositive();
    }

    [Test]
    public async Task Should_return_valid_view()
    {
        var result = await _controllerHelper.InitViewModel(string.Empty);
        result.Should().BeOfType<ComparisonToolPageViewModel>();
    }

    [Test]
    public async Task Should_return_valid_product_view()
    {
        _comparisonToolService.Setup(x => x.GetApprovedProductFromApprovedVendor(0))
            .ReturnsAsync(
                new ComparisonToolProduct()
                {
                    product_id = 0,
                    product_type = 1,
                    product_name = "test"
                }
            );
        var result = await _controllerHelper.InitViewModelForSelectedProduct(0);
        result.Should().BeOfType<ComparisonToolPageViewModel>();
    }

    [Test]
    public void Should_persist_comparison_tool_user_settings_none_set()
    {
        var model = new ComparisonToolPageViewModel();
        model.products = new List<ComparisonToolProduct>();
        _controllerHelper.SetViewModelUserJourneyData(model, string.Empty, string.Empty, string.Empty);
        model.SelectedProductId.Should().NotBeNull();
        model.SelectedProductCategoryId.Should().NotBeNull();
    }
    
    [Test]
    public void Should_persist_comparison_tool_user_settings_product_and_filter_set()
    {
        var model = new ComparisonToolPageViewModel();
        model.products = new List<ComparisonToolProduct>();
        _controllerHelper.SetViewModelUserJourneyData(model, "1", "0", "test");
        model.SelectedProductId.Should().Be("0");
        model.SelectedProductCategoryId.Should().Be("1");
    }

}