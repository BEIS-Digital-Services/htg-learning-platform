namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests
{
    public class ExpressionOfInterestControllerTests
    {
        private IExpressionOfInterestControllerHelper _ExpressionOfInterestControllerHelper;

        private Mock<ILogger<ExpressionOfInterestControllerHelper>> _logger;
        private Mock<IExpressionOfInterestService> _expressionOfInterestService;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<ExpressionOfInterestControllerHelper>>();
            _expressionOfInterestService = new Mock<IExpressionOfInterestService>();

            _ExpressionOfInterestControllerHelper = new ExpressionOfInterestControllerHelper(_logger.Object, _expressionOfInterestService.Object);
        }

        [Test]
        public async Task Should_return_bad_request_if_report_is_not_saved()
        {
            _expressionOfInterestService.Setup(e => e.SaveExpressionOfInterest(It.IsAny<System.Guid>(), It.IsAny<ExpressionOfInterestDto>()))
                .ReturnsAsync(GetServiceResponse(false));
            var result = await _ExpressionOfInterestControllerHelper.SaveExpressionOfInterest(new ExpressionOfInterestDto());
            result.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_return_ok_if_report_is_saved_successfully()
        {
            _expressionOfInterestService.Setup(e => e.SaveExpressionOfInterest(It.IsAny<System.Guid>(), It.IsAny<ExpressionOfInterestDto>()))
                .ReturnsAsync(GetServiceResponse(true));
            var result = await _ExpressionOfInterestControllerHelper.SaveExpressionOfInterest(new ExpressionOfInterestDto());
            result.Should().Be(HttpStatusCode.OK);
        }

        private static IServiceResponse<int> GetServiceResponse(bool valid)
        {
            var rtn = new ServiceResponse<int>(Guid.Empty, valid);
            return rtn;
        }

    }
}