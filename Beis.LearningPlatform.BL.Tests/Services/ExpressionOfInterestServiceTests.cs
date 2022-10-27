using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.Data.Entities.Skills;

namespace Beis.LearningPlatform.BL.Tests.Services
{
    public class ExpressionOfInterestServiceTests
    {
        // private IExpressionOfInterestDataService _expressionOfInterestDataService;
        private IExpressionOfInterestService _service;
        private Mock<IExpressionOfInterestDataService> _expressionOfInterestDataServiceMock;


        [SetUp]
        public void Setup()
        {
            _expressionOfInterestDataServiceMock = new Mock<IExpressionOfInterestDataService>();
            _service = new ExpressionOfInterestService(_expressionOfInterestDataServiceMock.Object);
        }


        [Test]
        public void SaveSkillsTwoResponse_ValidData_Success()
        {
            ExpressionOfInterestDto response = new ExpressionOfInterestDto
            {
                PageName = "Test Page",
                UserName = "Test User Name",
                UserBusinessName = "Test Business Name"

            };
            _service.SaveExpressionOfInterest(Guid.NewGuid(), response);
            _expressionOfInterestDataServiceMock.Verify(x => x.Add(It.IsAny<ExpressionOfInterestDto>()));
        }
    }
}
