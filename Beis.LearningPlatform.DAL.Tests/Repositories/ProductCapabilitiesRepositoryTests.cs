using Beis.HelpToGrow.Persistence;
using Beis.HelpToGrow.Persistence.Models;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories;
using FluentAssertions;
using Moq.EntityFrameworkCore;

namespace Beis.LearningPlatform.DAL.Tests.Repositories;

public class ProductCapabilitiesRepositoryTests
{
    private Mock<HtgVendorSmeDbContext> _context;
    private ProductCapabilitiesRepository _productFiltersRepository;

    [SetUp]
    public void Setup()
    {
        var capabilities = new List<product_capability>() { new() { product_id = 1}, new() { product_id = 2} };
        _context = new Mock<HtgVendorSmeDbContext>();
        _context.Setup(x => x.product_capabilities).ReturnsDbSet(capabilities);
        _productFiltersRepository = new ProductCapabilitiesRepository(_context.Object);
    }

    [Test]
    public async Task Should_return_single_capability()
    {
        var result = await _productFiltersRepository.GetProductCapabilitiesFilters(1);
        result.Should().BeOfType<List<product_capability>>();
        result.Count.Should().Be(1);
    }
    
    [Test]
    public async Task Should_return_all_capabilities()
    {
        var result = await _productFiltersRepository.GetProductCapabilitiesFilters();
        result.Should().BeOfType<List<product_capability>>();
        result.Count.Should().Be(2);
    }
}