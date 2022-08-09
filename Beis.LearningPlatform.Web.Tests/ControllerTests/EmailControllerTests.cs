namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class EmailControllerTests
    {
        private EmailController _controller;
        private Mock<ILogger<EmailController>> _logger;
        private Mock<ILogger<EmailControllerHelper>> _emailLogger;
        private Mock<IEmailService> _emailService;
        private Mock<IServiceResponse> _serviceResponse;
        private Mock<IServiceResponse> _serviceResponse1;

        private IEmailControllerHelper _emailControllerHelper;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<EmailController>>();
            _emailLogger = new Mock<ILogger<EmailControllerHelper>>();
            _emailService = new Mock<IEmailService>();
            _serviceResponse = new Mock<IServiceResponse>();
            _serviceResponse1 = new Mock<IServiceResponse>();
            _emailControllerHelper = new EmailControllerHelper(_emailLogger.Object, _emailService.Object); 
            _controller = new EmailController(_logger.Object, _emailControllerHelper);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void Should_throw_when_email_is_empty_null_whitespace(string emailAddress)
        {
            Func<Task> result = () => _controller.Unsubscribe(emailAddress);

            result.Should().ThrowAsync<Exception>().WithMessage("An unsubscribe email address must be specified");
        }

        [Test]
        public void Should_throw_if_email_is_invalid()
        {
            _serviceResponse.Setup(x => x.IsValid).Returns(false);
            _emailService.Setup(x => x.IsValidEmailAddress(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(_serviceResponse.Object);

            Func<Task> result = () => _controller.Unsubscribe("test");

            result.Should().ThrowAsync<Exception>().WithMessage("The unsubscribe email must be a valid email address");
        }

        [Test]
        public void Should_throw_if_service_to_unsubscribe_fails()
        {
            _serviceResponse.Setup(x => x.IsValid).Returns(true);
            _emailService.Setup(x => x.IsValidEmailAddress(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(_serviceResponse.Object);

            _serviceResponse1.Setup(x => x.IsValid).Returns(false);
            _emailService.Setup(x => x.UnsubscribeEmail(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(_serviceResponse1.Object);

            Func<Task> result = () => _controller.Unsubscribe("test@test.com");

            result.Should().ThrowAsync<Exception>().WithMessage("We were unable to unsubscribe your email address.  Please try again")
                .WithInnerException(typeof(InvalidOperationException)).WithMessage("Failed to unsubscribe email address");

            _emailLogger.Verify(x =>
                x.Log(LogLevel.Error, It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((m, c) => m.ToString() == "Unable to unsubscribe email address"), It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }

        [Test]
        public async Task Should_unsubscribe_successfully()
        {
            _serviceResponse.Setup(x => x.IsValid).Returns(true);
            _emailService.Setup(x => x.IsValidEmailAddress(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(_serviceResponse.Object);

            _emailService.Setup(x => x.UnsubscribeEmail(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(_serviceResponse.Object);

            var result = await _controller.Unsubscribe("test@test.com");
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ViewResult));

            var model = ((ViewResult)result).Model as PageViewModel;

            model.pageTitle.Should().Be("Help to Grow: Digital - Unsubscribe Email");
            model.pagename.Should().Be("Unsubscribe Email");
        }

        private void ValidateResult(IActionResult result)
        {
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ViewResult));

            var model = ((ViewResult)result).Model as ErrorViewModel;

            model.ErrorMessage.Should().NotBeNullOrEmpty();
            model.pageTitle.Should().Be("Help to Grow: Digital - Error");
            model.pagename.Should().Be("Error Page");
        }
    }
}