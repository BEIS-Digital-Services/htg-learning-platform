namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class ExpressionOfInterestControllerTests
    {
        private ExpressionOfInterestController _controller;

        private Mock<ILogger<ExpressionOfInterestController>> _logger;
        private Mock<IExpressionOfInterestControllerHelper> _expressionOfInterestControllerHelper;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<ExpressionOfInterestController>>();
            _expressionOfInterestControllerHelper = new Mock<IExpressionOfInterestControllerHelper>();
            _controller = new ExpressionOfInterestController(_logger.Object, _expressionOfInterestControllerHelper.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }

        [Test]
        public async Task Should_save_eoi_successfully_if_input_model_is_valid()
        {
            var result = await _controller.ExpressionOfInterest(new ExpressionOfInterestDto());
            var jsonResult = (JsonResult)result;
            
            _expressionOfInterestControllerHelper
                .Verify(x => x.SaveExpressionOfInterest(It.IsAny<ExpressionOfInterestDto>()),
                        Times.Once);
            jsonResult.Should().BeOfType(typeof(JsonResult));
        }

        [Test]
        public async Task Should_return_bad_request_for_process_report_if_input_model_is_invalid()
        {
            _controller.ModelState.AddModelError("test", "test");

            var result = await _controller.ExpressionOfInterest(new ExpressionOfInterestDto());
            var badRequestResult = (JsonResult)result;
            badRequestResult.Value.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}