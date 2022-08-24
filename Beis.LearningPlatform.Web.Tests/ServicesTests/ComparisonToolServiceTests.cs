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
        _productRepository.Setup(x => x.GetProducts()).ReturnsAsync(
            new List<product>()
            {
                new product()
                {
                    product_id = 1,
                    vendor_id = 2
                }
            }
        );
        _pricingRepository.Setup(x => x.GetAllProductPricesForProductId(1)).ReturnsAsync(
                new List<product_price>()
                {
                    new product_price()
                    {
                        productid = 1,
                        product_price_id = 1
                    }
                }
            );
        _pricingRepository.Setup(x => x.GetAllProductBaseMetricPricesByProductPriceId(1)).ReturnsAsync(
            new List<product_price_base_metric_price>()
            {
                new product_price_base_metric_price()
                {
                    product_price_id = 1
                }
            }
        );
        _mapper.Setup(x => x.Map<ComparisonToolProduct>(It.IsAny<product[]>()))
            .Returns(
                new ComparisonToolProduct()
            );
        var result = await _comparisonToolService.GetProducts();
        result.Should().NotBeNull();
        result.Should().BeOfType<List<ComparisonToolProduct>>();
        result.Count().Should().Be(1);
    }
}