using Beis.HelpToGrow.Persistence.Models;
using Beis.LearningPlatform.Data.Entities.SatisfactionSurvey;
using Microsoft.EntityFrameworkCore;

namespace Beis.LearningPlatform.Tests
{
    public class SatisfactionSurveyControllerTests : BaseControllerTests
    {
        private SatisfactionSurveyController _controller;
        private Mock<ICmsService2> _cmsService2;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private Mock<HttpContext> _httpContext;
        private Mock<HttpRequest> _httpRequest;

        [SetUp]
        public void SetUp()
        {
            _cmsService2 = new Mock<ICmsService2>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _httpContext = new Mock<HttpContext>();
            _httpRequest = new Mock<HttpRequest>();
            var satisfactionSurveyControllerHelper = new SatisfactionSurveyControllerHelper(
                new Mock<ILogger<ControllerHelperBase>>().Object,
                ServiceProvider.GetService<ISatisfactionSurveyService>(),
                ServiceProvider.GetService<IMapper>());
            _controller = new SatisfactionSurveyController(satisfactionSurveyControllerHelper, _cmsService2.Object);
        }

        [Test]
        public async Task Should_start_survey()
        {
            // Arrange
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                .ReturnsAsync(new CMSPageViewModel());
            const string refererUrl = "https://www.test.com";
            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpRequest.SetupGet(x => x.Headers)
               .Returns(new HeaderDictionary { { "Referer", refererUrl } });

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };

            // Act
            var result = await _controller.Index();

            // Assert
            var model = ValidateResult(result);
            model.Data.Url.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public async Task Should_complete_survey()
        {
            // Arrange
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                .ReturnsAsync(new CMSPageViewModel());

            // Act
            var result = await _controller.Complete();

            // Assert
            ValidateResult(result);
        }

        [Test]
        public async Task Should_return_bad_request_if_no_rating_options_are_given()
        {
            // Arrange, Act
            var result = await _controller.SubmitSurvey(new SatisfactionSurveyViewModel());

            // Assert
            var badRequestResult = (BadRequestResult)result;
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_return_error_when_submitting_survey_for_api_connection_failure()
        {
            // Arrange
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                .ReturnsAsync(new CMSPageViewModel());
            SetupSatisfactionSurveyEntry();

            // Act
            var result = await _controller.SubmitSurvey(new SatisfactionSurveyViewModel()
            {
                Rating = "Satisfied"
            });

            // Assert
            ValidateResult(result);
        }

        [Test]
        public async Task Should_not_submit_survey_when_model_is_invalid()
        {
            // Arrange
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                    .ReturnsAsync(new CMSPageViewModel());
            _controller.ModelState.AddModelError("test", "test");

            // Act
            var result = await _controller.SubmitSurvey(new SatisfactionSurveyViewModel()
            {
                Rating = "Satisfied"
            });

            // Assert
            ValidateResult(result);
        }

        [Test]
        public async Task Should_redirect_to_complete_when_submitting_survey_successfully()
        {
            // Arrange
            _cmsService2.Setup(x => x.GetPage(It.IsAny<string>()))
                .ReturnsAsync(new CMSPageViewModel());
            SetupSatisfactionSurveyEntry(true);

            // Act
            var result = await _controller.SubmitSurvey(new SatisfactionSurveyViewModel
            {
                Rating = "Satisfied",
                Comment = "TestComment",
                Url = "www.testurl.com"
            });

            // Assert
            result.Should().BeOfType(typeof(RedirectToActionResult));
            ((RedirectToActionResult)result).ActionName.Should().Be("Complete");
            MockLpDbContext.Object.SatisfactionSurveyEntry.Count().Should().Be(2);
            MockLpDbContext.Verify(r => r.SaveChangesAsync(default), Times.Once);
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