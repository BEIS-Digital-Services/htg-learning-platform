using Beis.HelpToGrow.Persistence;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories;
using Beis.LearningPlatform.DAL.Repositories.ProductRepositories.Interface;
using Beis.LearningPlatform.Data.Entities.Skills;
using Beis.LearningPlatform.Data.Repositories.DiagnosticTool;

namespace Beis.LearningPlatform.DAL.Tests
{
    public class ProductRepositoryTests
    {
        private Mock<HtgVendorSmeDbContext> _contextMock;
        private IProductRepository _productRepositoryMock;
     
        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<HtgVendorSmeDbContext>();
            _productRepositoryMock = new ProductRepository(_contextMock.Object);

        }

        [Test]
        public void GetApprovedProducts_Test()
        {
          var list =  _productRepositoryMock.GetApprovedProducts();
          Assert.IsNotNull(list);
          
        }
    }
}