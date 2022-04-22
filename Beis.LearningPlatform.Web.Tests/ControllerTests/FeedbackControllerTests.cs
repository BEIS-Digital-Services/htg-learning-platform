using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Controllers;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class FeedbackControllerTests
    {
        private FeedbackController _controller;

        private Mock<ILogger<FeedbackController>> _logger;
        private Mock<IFeedbackControllerHelper> _feedbackControllerHelper;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<FeedbackController>>();
            _feedbackControllerHelper = new Mock<IFeedbackControllerHelper>();
            _controller = new FeedbackController(_logger.Object, _feedbackControllerHelper.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var response = _controller.ControllerContext.HttpContext.Response;
        }

        [Test]
        public async Task Should_report_problem_successfully_if_input_model_is_valid()
        {
            var result = await _controller.ProcessReport(new CMSFeedbackProblem());
            var jsonResult = (JsonResult)result;
            
            _feedbackControllerHelper
                .Verify(x => x.ReportProblem(It.IsAny<CMSFeedbackProblem>()),
                        Times.Once);
            jsonResult.Should().BeOfType(typeof(JsonResult));
        }

        [Test]
        public async Task Should_return_bad_request_for_process_report_if_input_model_is_invalid()
        {
            _controller.ModelState.AddModelError("test", "test");

            var result = await _controller.ProcessReport(new CMSFeedbackProblem());
            var badRequestResult = (JsonResult)result;
            badRequestResult.Value.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_proces_feedbackuse_successfully_if_input_model_is_valid()
        {
            var result = await _controller.ProcessFeedbackUse(new CMSFeedbackPageUseful());
            var jsonResult = (JsonResult)result;

            _feedbackControllerHelper
                .Verify(x => x.IsUseful(It.IsAny<CMSFeedbackPageUseful>()),
                        Times.Once);
            jsonResult.Should().BeOfType(typeof(JsonResult));
        }

        [Test]
        public async Task Should_return_bad_request_for_process_feedbackuse_if_input_model_is_invalid()
        {
            _controller.ModelState.AddModelError("test", "test");

            var result = await _controller.ProcessFeedbackUse(new CMSFeedbackPageUseful());
            var badRequestResult = (JsonResult)result;
            badRequestResult.Value.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_proces_feedback_successfully_if_input_is_valid()
        {
            var redirectUrl = "/testurl";
            _feedbackControllerHelper.Setup(x => x.GetFeedbackRouteUrl())
                .Returns(redirectUrl);

            var result = await _controller.Feedback("test");
           
            _feedbackControllerHelper
                .Verify(x => x.ProcessFeedback(It.IsAny<string>()),
                        Times.Once);
            _feedbackControllerHelper .Verify(x => x.GetFeedbackRouteUrl(), Times.Once);
            result.Should().BeOfType(typeof(RedirectResult));
            ((RedirectResult)result).Url.Should().NotBeNullOrEmpty();
            ((RedirectResult)result).Url.Should().Be(redirectUrl);
        }

        [Test]
        public async Task Should_return_bad_request_for_process_feedback_if_input_is_invalid()
        {
            var result = await _controller.Feedback(null);
            var badRequestResult = (BadRequestResult)result;
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}