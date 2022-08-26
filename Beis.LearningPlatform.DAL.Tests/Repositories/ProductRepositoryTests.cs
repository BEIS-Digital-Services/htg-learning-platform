using Beis.HelpToGrow.Persistence;
using Beis.HelpToGrow.Persistence.Models;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace Beis.LearningPlatform.DAL.Tests.Repositories;

public class ProductRepositoryTests
{
    private Mock<HtgVendorSmeDbContext> _context;
    private ProductRepository _productRepository;

    [SetUp]
    public void Setup()
    {
        var products = new List<product>() { new() { product_id = 1, vendor_id = 1, status = 50} };
        var vendors = new List<vendor_company>() { new() { vendorid = 1, application_status = 50} };
        var status = new List<product_status>() { new() { id = 50, status_description = "approved"} };
        var vendorStatus = new List<vendor_status>() { new() { id = 50, status_description = "approved"} };
        _context = new Mock<HtgVendorSmeDbContext>();
        _context.Setup(x => x.products).ReturnsDbSet(products);
        _context.Setup(x => x.vendor_companies).ReturnsDbSet(vendors);
        _context.Setup(x => x.product_statuses).ReturnsDbSet(status);
        _context.Setup(x => x.vendor_statuses).ReturnsDbSet(vendorStatus);
        _productRepository = new ProductRepository(_context.Object);
    }

    [Test]
    public async Task Should_get_list_of_products()
    {
        var result = await _productRepository.GetProducts();
        result.Should().BeOfType<List<product>>();
        result.Should().NotBeNull();
        result.Count().Should().BePositive();
    }

    [Test]
    public async Task Should_return_list_of_approved_products()
    {
        var result = await _productRepository.GetApprovedProducts();
        result.Should().BeOfType<List<product>>();
        result.Should().NotBeNull();
        result.Count().Should().BePositive();
    }

    [Test]
    public async Task Should_return_list_of_approved_vendor_products()
    {
        var result = await _productRepository.GetApprovedProductsFromApprovedVendors();
        result.Should().BeOfType<List<product>>();
        result.Should().NotBeNull();
        result.Count().Should().BePositive();
    }

    [Test]
    public async Task Should_return_matched_product()
    {
        var result = await _productRepository.GetProduct(1);
        result.Should().BeOfType<product>();
        result.Should().NotBeNull();
        result.product_id.Should().Be(1);
    }

    [Test]
    public async Task Should_return_approved_product()
    {
        var result = await _productRepository.GetApprovedProductFromApprovedVendor(1);
        result.Should().BeOfType<product>();
        result.Should().NotBeNull();
        result.product_id.Should().Be(1);
    }
}