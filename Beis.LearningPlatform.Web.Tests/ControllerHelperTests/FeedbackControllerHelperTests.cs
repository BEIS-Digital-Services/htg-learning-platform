using AutoMapper;
using Beis.LearningPlatform.BL.Models;
using Beis.LearningPlatform.BL.Services;
using Beis.LearningPlatform.Web.ControllerHelpers;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.Services;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests
{
    public class FeedbackControllerHelperTests
    {
        private IFeedbackControllerHelper _feedbackControllerHelper;

        private Mock<ILogger<FeedbackControllerHelper>> _logger;
        private Mock<ICmsService> _cmsService;
        private CmsFeedbackService _cmsFeedbackService;
        private IOptions<CmsOption> _cmsOptions;
        private Mock<IMapper> _mapper;
        private Mock<IFeedbackService> _feedbackService;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private Mock<HttpContext> _httpContext;
        private Mock<ISession> _session;
        private Mock<HttpRequest> _httpRequest;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<FeedbackControllerHelper>>();
            _cmsService = new Mock<ICmsService>();
            _feedbackService = new Mock<IFeedbackService>();

            _cmsFeedbackService = new CmsFeedbackService(_feedbackService.Object);
            _cmsOptions = ConfigOptions.Create(new CmsOption());
            _mapper = new Mock<IMapper>();


            _session = new Mock<ISession>();
            _httpContext = new Mock<HttpContext>();
            _httpRequest = new Mock<HttpRequest>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _feedbackControllerHelper = new FeedbackControllerHelper(_logger.Object, _cmsService.Object, _cmsFeedbackService, _cmsOptions,
                  _mapper.Object, _httpContextAccessor.Object);
        }

        [Test]
        public async Task Should_return_bad_request_if_report_is_not_saved()
        {
            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _httpContext.SetupGet(x => x.Session)
                .Returns(_session.Object);


             _session.Setup(x => x.Id).Returns("sessionId");
            _mapper.Setup(x => x.Map<CMSFeedbackProblemBM>(It.IsAny<CMSFeedbackProblem>())).Returns(new CMSFeedbackProblemBM());
            _feedbackService.Setup(x => x.SaveFeedBackReport(It.IsAny<CMSFeedbackProblemBM>())).ReturnsAsync(false);

            var result = await _feedbackControllerHelper.ReportProblem(new CMSFeedbackProblem());
            result.Should().Be(HttpStatusCode.BadRequest);

        }

        [Test]
        public async Task Should_return_ok_if_report_is_saved_successfully()
        {
            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _httpContext.SetupGet(x => x.Session)
                .Returns(_session.Object);


            _session.Setup(x => x.Id).Returns("sessionId");
            _mapper.Setup(x => x.Map<CMSFeedbackProblemBM>(It.IsAny<CMSFeedbackProblem>())).Returns(new CMSFeedbackProblemBM());
            _feedbackService.Setup(x => x.SaveFeedBackReport(It.IsAny<CMSFeedbackProblemBM>())).ReturnsAsync(true);

            var result = await _feedbackControllerHelper.ReportProblem(new CMSFeedbackProblem());
            result.Should().Be(HttpStatusCode.OK);

        }


        [Test]
        public async Task Should_return_bad_request_if_feedback_page_useful_is_not_saved()
        {
            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _httpContext.SetupGet(x => x.Session)
                .Returns(_session.Object);


            _session.Setup(x => x.Id).Returns("sessionId");
            _mapper.Setup(x => x.Map<CMSFeedbackPageUsefulBM>(It.IsAny<CMSFeedbackPageUseful>())).Returns(new CMSFeedbackPageUsefulBM());
            _feedbackService.Setup(x => x.SaveFeedBackPageUseful(It.IsAny<CMSFeedbackPageUsefulBM>())).ReturnsAsync(false);

            var result = await _feedbackControllerHelper.IsUseful(new CMSFeedbackPageUseful());
            result.Should().Be(HttpStatusCode.BadRequest);

        }

        [Test]
        public async Task Should_return_ok_if_feedback_page_useful_is_saved_successfully()
        {
            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _httpContext.SetupGet(x => x.Session)
                .Returns(_session.Object);


            _session.Setup(x => x.Id).Returns("sessionId");
            _mapper.Setup(x => x.Map<CMSFeedbackPageUsefulBM>(It.IsAny<CMSFeedbackPageUseful>())).Returns(new CMSFeedbackPageUsefulBM());
            _feedbackService.Setup(x => x.SaveFeedBackPageUseful(It.IsAny<CMSFeedbackPageUsefulBM>())).ReturnsAsync(true);

            var result = await _feedbackControllerHelper.IsUseful(new CMSFeedbackPageUseful());
            result.Should().Be(HttpStatusCode.OK);

        }

        [Test]
        public async Task Should_return_true_if_feedback_page_useful_is_saved_successfully()
        {
            var refererUrl = "https://www.test.com";

            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpRequest.SetupGet(x => x.Headers)
               .Returns(new HeaderDictionary { { "Referer", refererUrl } });


            _feedbackService.Setup(x => x.SaveFeedBackPageUseful(It.IsAny<CMSFeedbackPageUsefulBM>())).ReturnsAsync(true);

            var result = await _feedbackControllerHelper.ProcessFeedback(It.IsAny<string>());
            result.Should().Be(true);

        }

        [Test]
        public async Task Should_return_false_if_feedback_page_useful_is_not_saved_successfully()
        {
            var refererUrl = "https://www.test.com";

            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpRequest.SetupGet(x => x.Headers)
               .Returns(new HeaderDictionary { { "Referer", refererUrl } });


            _feedbackService.Setup(x => x.SaveFeedBackPageUseful(It.IsAny<CMSFeedbackPageUsefulBM>())).ReturnsAsync(false);

            var result = await _feedbackControllerHelper.ProcessFeedback(It.IsAny<string>());
            result.Should().Be(false);

        }

        [Test]
        public void Should_return_valid_feedback_url()
        {
            var refererUrl = "https://www.test.com";

            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(_httpContext.Object);
            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpRequest.SetupGet(x => x.Headers)
               .Returns(new HeaderDictionary { { "Referer", refererUrl } });

            var result = _feedbackControllerHelper.GetFeedbackRouteUrl();
            result.Should().NotBeNullOrEmpty(result);

        }
    }
}