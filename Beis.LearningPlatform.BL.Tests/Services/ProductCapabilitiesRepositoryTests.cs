using Beis.HelpToGrow.Persistence;
using Beis.HelpToGrow.Persistence.Models;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;

namespace Beis.LearningPlatform.DAL.Tests.ProductRepositories
{
    public class ProductCapabilitiesRepositoryTests
    {
        [Test]
        public void GetProductCapabilitiesFilters()
        {
            var repository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            var mockFoo = repository.Create<product_capability>();
            var dbContextMock = new Mock<HtgVendorSmeDbContext>();
            var dbSetMock = new Mock<DbSet<product_capability>>();
            dbContextMock.Setup(s => s.Set<product_capability>()).Returns(dbSetMock.Object);
            
            var productRepository = new ProductCapabilitiesRepository(dbContextMock.Object);
            var products = productRepository.GetProductCapabilitiesFilters(0).Result;

            //Assert  
            Assert.NotNull(products);
            Assert.IsAssignableFrom<product_capability>(products);
        }
    }
}