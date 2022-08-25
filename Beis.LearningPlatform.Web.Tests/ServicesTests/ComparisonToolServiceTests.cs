using Beis.HelpToGrow.Persistence.Models;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Pricing;

namespace Beis.LearningPlatform.Web.Tests.ServicesTests;

public class ComparisonToolServiceTests
{
    private ComparisonToolService _comparisonToolService;
    private Mock<IMapper> _mapper;
    private Mock<ICmsService> _cmsService;
    private Mock<IProductRepository> _productRepository;
    private Mock<IPricingRepository> _pricingRepository;
    private Mock<ISettingsProductCapabilitiesRepository> _settingsProductCapabilitiesRepository;
    private Mock<IProductCapabilitiesRepository> _productCapabilitiesRepository;
    private Mock<ISettingsProductFiltersRepository> _settingsProductFiltersRepository;
    private Mock<IProductFiltersRepository> _productFiltersRepository;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mock<IMapper>();
        _cmsService = new Mock<ICmsService>();
        _productRepository = new Mock<IProductRepository>();
        _settingsProductCapabilitiesRepository = new Mock<ISettingsProductCapabilitiesRepository>();
        _productCapabilitiesRepository = new Mock<IProductCapabilitiesRepository>();
        _pricingRepository = new Mock<IPricingRepository>();
        _settingsProductFiltersRepository = new Mock<ISettingsProductFiltersRepository>();
        _productFiltersRepository = new Mock<IProductFiltersRepository>();
        _comparisonToolService = new ComparisonToolService(
                _mapper.Object,
                _cmsService.Object,
                _productRepository.Object,
                _productCapabilitiesRepository.Object,
                _pricingRepository.Object,
                _settingsProductCapabilitiesRepository.Object,
                _settingsProductFiltersRepository.Object,
                _productFiltersRepository.Object
            );
    }

    [Test]
    public async Task Should_return_empty_products_list()
    {
        _productRepository.Setup(x => x.GetProducts()).ReturnsAsync(
                new List<product>()
            );
        var result = await _comparisonToolService.GetProducts();
        result.Should().NotBeNull();
        result.Should().BeOfType<List<ComparisonToolProduct>>();
    }

    [Test]
    public async Task Should_return_populated_products_list()
    {
        _productRepository.Setup(x => x.GetProducts())
            .ReturnsAsync(new List<product>() { new() { product_id = 1, vendor_id = 2 } });
        _pricingRepository.Setup(x => x.GetAllProductPricesForProductId(1))
            .ReturnsAsync(new List<product_price>() { new() { productid = 1, product_price_id = 1 } });
        _pricingRepository.Setup(x => x.GetAllProductBaseMetricPricesByProductPriceId(1))
            .ReturnsAsync(new List<product_price_base_metric_price>() { new() { product_price_id = 1 } }
        );
        _mapper.Setup(x => x.Map<ComparisonToolProduct>(It.IsAny<product[]>()))
            .Returns(new ComparisonToolProduct(){ product_id = 1, vendor_id = 2});
        var result = await _comparisonToolService.GetProducts();
        result.Should().NotBeNull();
        result.Should().BeOfType<List<ComparisonToolProduct>>();
    }

    [Test]
    public async Task Should_return_product()
    {
        _productRepository.Setup(x => x.GetProduct(1)).ReturnsAsync(
             new product() { product_id = 1, vendor_id = 2 }
        );
        _pricingRepository.Setup(x => x.GetAllProductPricesForProductId(1))
            .ReturnsAsync(new List<product_price>() { new() { productid = 1, product_price_id = 1 } });
        _pricingRepository.Setup(x => x.GetAllProductBaseMetricPricesByProductPriceId(1))
            .ReturnsAsync(new List<product_price_base_metric_price>() { new() { product_price_id = 1 } }
            );
        _mapper.Setup(x => x.Map<ComparisonToolProduct>(It.IsAny<product>()))
            .Returns(new ComparisonToolProduct(){ product_id = 1, vendor_id = 2});
        var result = await _comparisonToolService.GetProduct(1);
        result.Should().NotBeNull();
        result.product_id.Should().Be(1);
    }
    
    [Test]
    public async Task Should_return_populated_approved_products_list()
    {
        _productRepository.Setup(x => x.GetApprovedProductsFromApprovedVendors())
            .ReturnsAsync(new List<product>() { new() { product_id = 1, vendor_id = 2 } });
        _pricingRepository.Setup(x => x.GetAllProductPricesForProductId(1))
            .ReturnsAsync(new List<product_price>() { new() { productid = 1, product_price_id = 1 } });
        _pricingRepository.Setup(x => x.GetAllProductBaseMetricPricesByProductPriceId(1))
            .ReturnsAsync(new List<product_price_base_metric_price>() { new() { product_price_id = 1 } }
            );
        _mapper.Setup(x => x.Map<ComparisonToolProduct>(It.IsAny<product>()))
            .Returns(new ComparisonToolProduct());
        var result = await _comparisonToolService.GetApprovedProductsFromApprovedVendors();
        result.Should().NotBeNull();
        result.Should().BeOfType<List<ComparisonToolProduct>>();
    }

    [Test]
    public async Task Should_return_approved_product()
    {
        _productRepository.Setup(x => x.GetApprovedProductFromApprovedVendor(1)).ReturnsAsync(
            new product() { product_id = 1, vendor_id = 2 });
        _pricingRepository.Setup(x => x.GetAllProductPricesForProductId(1))
            .ReturnsAsync(new List<product_price>() { new() { productid = 1, product_price_id = 1 } });
        _pricingRepository.Setup(x => x.GetAllProductBaseMetricPricesByProductPriceId(1))
            .ReturnsAsync(new List<product_price_base_metric_price>() { new() { product_price_id = 1 } }
            );
        _mapper.Setup(x => x.Map<ComparisonToolProduct>(It.IsAny<product>()))
            .Returns(new ComparisonToolProduct(){ product_id = 1, vendor_id = 2});
        var result = await _comparisonToolService.GetApprovedProductFromApprovedVendor(1);
        result.Should().NotBeNull();
        result.product_id.Should().Be(1);
    }

    [Test]
    public async Task Should_populate_nav_footer_from_CMS()
    {
        _cmsService.Setup(x => x.GetComparisonToolPageResult("Comparison-tools"))
            .ReturnsAsync(new List<ComparisonToolPageViewModel>()
            {
                new ComparisonToolPageViewModel()
                {
                    footers =new List<CMSPageFooter>(){ new CMSPageFooter()},
                    navigations = new List<CMSPageNavigation>(){ new CMSPageNavigation()}
                }
            });
        var view = new ComparisonToolPageViewModel();
        var result = await _comparisonToolService.SetNavAndFooter(view);
        result.Should().BeTrue();
        view.navigations.Should().NotBeNull();
        view.footers.Should().NotBeNull();
    }

    [Test]
    public async Task Should_populate_product_details()
    {
        _settingsProductCapabilitiesRepository.Setup(x => x.GetSettingsProductCapabilities())
            .ReturnsAsync(new List<settings_product_capability>() { new() { product_type = 1 } });
        _productFiltersRepository.Setup(x => x.GetProductFilters(1))
            .ReturnsAsync(new List<product_filter>() { new() { filter_id = 1, product_id = 1 } });
        _settingsProductFiltersRepository.Setup(x => x.GetSettingsProductFilters(It.IsAny<long>()))
            .ReturnsAsync(new List<settings_product_filter>() { new() { filter_id = 1} });
        var products = new List<ComparisonToolProduct>()
            { new ComparisonToolProduct() { product_id = 1, 
                vendor_id = 1, 
                product_type = 1, 
                productPrices = new List<product_price>() }};
        await _comparisonToolService.PopulateChildRelationships(products);
        products.Should().NotBeNull();
        products[0].Should().BeOfType<ComparisonToolProduct>();
    }
}