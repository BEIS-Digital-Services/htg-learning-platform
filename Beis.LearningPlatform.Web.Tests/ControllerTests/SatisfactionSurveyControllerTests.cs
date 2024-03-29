﻿namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class SatisfactionSurveyControllerTests
    {
        private ISatisfactionSurveyControllerHelper _satisfactionSurveyControllerHelper;
        private Mock<ICmsService2> _cmsService2;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private Mock<HttpContext> _httpContext;
        private Mock<HttpRequest> _httpRequest;
        private Mock<IMapper> _mockMapper;
        private Mock<ISatisfactionSurveyService> _mockSatisfactionSurveyService;
        private SatisfactionSurveyController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockSatisfactionSurveyService = new Mock<ISatisfactionSurveyService>();
            _satisfactionSurveyControllerHelper =
                new SatisfactionSurveyControllerHelper(
                    new Mock<ILogger<SatisfactionSurveyControllerHelper>>().Object,
                    _mockSatisfactionSurveyService.Object,
                    _mockMapper.Object);
            _cmsService2 = new Mock<ICmsService2>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _httpContext = new Mock<HttpContext>();
            _httpRequest = new Mock<HttpRequest>();
            _controller = new SatisfactionSurveyController(_satisfactionSurveyControllerHelper,
                _cmsService2.Object, _httpContextAccessor.Object);
        }

        [Test]
        public async Task Should_start_survey()
        {
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                .ReturnsAsync(new CMSPageViewModel());

            var refererUrl = "https://www.test.com";

            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpRequest.SetupGet(x => x.Headers)
               .Returns(new HeaderDictionary { { "Referer", refererUrl } });

            var result = await _controller.Index();
            var model = ValidateResult(result);

            Assert.IsNotEmpty(model.Data.Url);
            
        }

        [Test]
        public async Task Should_complete_survey()
        {
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                .ReturnsAsync(new CMSPageViewModel());

            var result = await _controller.Complete();

            ValidateResult(result);

        }

        [Test]
        public async Task Should_return_bad_request_if_no_rating_options_are_given()
        {
            var result = await _controller.SubmitSurvey(new SatisfactionSurveyViewModel());
            var badRequestResult = (BadRequestResult)result;
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_return_error_when_submitting_survey_for_api_connection_failure()
        {
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                .ReturnsAsync(new CMSPageViewModel());

            _mockSatisfactionSurveyService
                .Setup(s => s.SaveSatisfactionSurvey(It.IsAny<Guid>(), It.IsAny<SatisfactionSurveyDto>()))
                .ReturnsAsync(new ServiceResponse<int>(It.IsAny<Guid>(), false));


            var result = await _controller.SubmitSurvey(new SatisfactionSurveyViewModel()
            {
                Rating = "Satisfied"
            });

            ValidateResult(result);

            _mockSatisfactionSurveyService
                .Verify(x => x.SaveSatisfactionSurvey(
                    It.IsAny<Guid>(), It.IsAny<SatisfactionSurveyDto>()), Times.Once);
        }

        [Test]
        public async Task Should_not_submit_survey_when_model_is_invalid()
        {
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                    .ReturnsAsync(new CMSPageViewModel());

            _controller.ModelState.AddModelError("test", "test");
            var result = await _controller.SubmitSurvey(new SatisfactionSurveyViewModel()
            {
                Rating = "Satisfied"
            });

            ValidateResult(result);

            _mockSatisfactionSurveyService
                .Verify(x => x.SaveSatisfactionSurvey(
                    It.IsAny<Guid>(), It.IsAny<SatisfactionSurveyDto>()), Times.Never);
        }

        [Test]
        public async Task Should_redirect_to_complete_when_submitting_survey_successfully()
        {
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                .ReturnsAsync(new CMSPageViewModel());

            _mockMapper.Setup(m => m.Map<SatisfactionSurveyDto>(It.IsAny<SatisfactionSurveyViewModel>()))
                .Returns(new SatisfactionSurveyDto());

            _mockSatisfactionSurveyService
                .Setup(s => s.SaveSatisfactionSurvey(It.IsAny<Guid>(), It.IsAny<SatisfactionSurveyDto>()))
                .ReturnsAsync(new ServiceResponse<int>(It.IsAny<Guid>(), true));

            var result = await _controller.SubmitSurvey(new SatisfactionSurveyViewModel()
            {
                Rating = "Satisfied"
            });

            result.Should().BeOfType(typeof(RedirectToActionResult));
            ((RedirectToActionResult)result).ActionName.Should().Be("Complete");
        }

        private static DataPageViewModel<SatisfactionSurveyViewModel> ValidateResult(IActionResult result)
        {
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ViewResult));

            var model = ((ViewResult)result).Model as DataPageViewModel<SatisfactionSurveyViewModel>;

            model.pageTitle.Should().Be("Help to Grow: Digital - Satisfaction Survey");
            model.pagename.Should().Be(null); // Not in top nav

            return model;
        }
    }
}