using Beis.LearningPlatform.BL.Domain;
using Beis.LearningPlatform.BL.Services;
using Beis.LearningPlatform.Web.ControllerHelpers;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Controllers;
using Beis.LearningPlatform.Web.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

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

        [Test]
        public async Task Should_return_to_error_page_if_email_is_empty()
        {
            
            var result = await _controller.Unsubscribe("");

            ValidateResult(result);

        }

        [Test]
        public async Task Should_return_to_error_page_if_email_is_invalid()
        {
            
            _serviceResponse.Setup(x => x.IsValid).Returns(false);
            _emailService.Setup(x => x.IsValidEmailAddress(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(_serviceResponse.Object);

            var result = await _controller.Unsubscribe("test");

            ValidateResult(result);

        }

        [Test]
        public async Task Should_return_to_error_page_if_service_to_unsubscribe_fails()
        {
            _serviceResponse.Setup(x => x.IsValid).Returns(true);
            _emailService.Setup(x => x.IsValidEmailAddress(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(_serviceResponse.Object);

            _serviceResponse1.Setup(x => x.IsValid).Returns(false);
            _emailService.Setup(x => x.UnsubscribeEmail(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(_serviceResponse1.Object);

            var result = await _controller.Unsubscribe("test@test.com");
            ValidateResult(result);
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