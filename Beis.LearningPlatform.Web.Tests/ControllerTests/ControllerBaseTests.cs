using ControllerBase = Beis.LearningPlatform.Web.Controllers.ControllerBase;

namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class ControllerBaseTests
    {
        private Mock<ILogger<TestController>> _logger;
        private TestController _controller;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<TestController>>();
            _controller = new TestController(_logger.Object);
        }

        [Test]
        public void Test_ReturnErrorPage_Works()
        {
            var requestId = new Guid();
            var errorMessage = "error message";
            var sessionId = "session id";

            var result = _controller.ReturnErrorPage(requestId, errorMessage, sessionId);

            Assert.IsNotNull(result);

            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewName, Is.EqualTo("Error"));
            Assert.IsNotNull(viewResult.Model);
            Assert.That(viewResult.Model, Is.TypeOf<ErrorViewModel>());

            var pageViewModel = viewResult.Model as ErrorViewModel;
            Assert.IsNotNull(pageViewModel);
            Assert.That(pageViewModel.RequestId, Is.EqualTo(requestId.ToString()));
            Assert.That(pageViewModel.ErrorMessage, Is.EqualTo(errorMessage));
            Assert.That(pageViewModel.SessionId, Is.EqualTo(sessionId));
        }
    }

    public class TestController : ControllerBase
    {
        public TestController(ILogger<TestController> logger) : base(logger)
        {
        }
    }
}