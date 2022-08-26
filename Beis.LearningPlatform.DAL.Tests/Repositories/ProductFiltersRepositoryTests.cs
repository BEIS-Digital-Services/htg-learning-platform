using Beis.HelpToGrow.Persistence;
using Beis.HelpToGrow.Persistence.Models;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories;
using FluentAssertions;
using Moq.EntityFrameworkCore;

namespace Beis.LearningPlatform.DAL.Tests.Repositories;

public class ProductFiltersRepositoryTests
{
    private Mock<HtgVendorSmeDbContext> _context;
    private ProductFiltersRepository _productFiltersRepository;

    [SetUp]
    public void Setup()
    {
        var filters = new List<product_filter>() { new() { product_id = 1} };
        _context = new Mock<HtgVendorSmeDbContext>();
        _context.Setup(x => x.product_filters).ReturnsDbSet(filters);
        _productFiltersRepository = new ProductFiltersRepository(_context.Object);
    }

    [Test]
    public async Task Should_return_filters_list()
    {
        var result = await _productFiltersRepository.GetProductFilters(1);
        result.Should().BeOfType<List<product_filter>>();
        result.Count.Should().BePositive();
    }
}