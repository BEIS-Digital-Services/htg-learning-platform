using AutoFixture;
using System.IO.Abstractions;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ServicesTests
{
    // NOTE: Check for SocketException: No such host is known to ensure strapi host exist
    public class CookieServiceTests
    {
        private Mock<IFileSystem> _fileSystem;
        private Mock<ILogger<DiagnosticToolController>> _logger;
        private Mock<ILogger<CmsService>> _cmsLogger;
        private Mock<IConfiguration> _Configuration;
        private Mock<ICmsService> _CmsService;
        private readonly IOptions<CmsOption> _cmsOptions = ConfigOptions.Create(new CmsOption());
        private Mock<IFeedbackService> _FeedbackService;
        private Mock<ICookieService> _CookieService;
        private StrapiMakeApiCallMockService _MakeApiCallService;
        private readonly IOptions<CookieNamesOption> _cookieNamesOptions = ConfigOptions.Create(new CookieNamesOption());
        private Mock<IHttpContextAccessor> _httpContextAccessor;

        private ICmsService cmsService;
        private CookieService cookieService;

        private Fixture _fixture;
        private HttpContext httpContext = new DefaultHttpContext();
        private Mock<ITempDataDictionary> _TempData;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fileSystem = new Mock<IFileSystem>();
            _logger = new Mock<ILogger<DiagnosticToolController>>();
            _cmsLogger =  new Mock<ILogger<CmsService>>();
            _Configuration = new Mock<IConfiguration>();
            _CmsService = new Mock<ICmsService>();
            _FeedbackService = new Mock<IFeedbackService>();
            _CookieService = new Mock<ICookieService>();
            _MakeApiCallService = new StrapiMakeApiCallMockService();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            cmsService = new CmsService(_cmsLogger.Object, _cmsOptions, _MakeApiCallService);
            cookieService = new CookieService(_cookieNamesOptions, _httpContextAccessor.Object);

            _TempData = new Mock<ITempDataDictionary>();
        }

        [Test]
        public void Should_set_cookie()
        {
            try
            {
                cookieService.ProcessCookie("HTG", true);
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, got: " + e.Message);
            }
            
        }
        
        [Test]
        public void Should_set_banner_closed_cookie()
        {
            try
            {
                cookieService.ProcessCookie("close", true);
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, got: " + e.Message);
            }
            
        }

        [Test]
        public void Should_fail_invalid_cookie_name()
        {
            try
            {
                cookieService.ProcessCookie(String.Empty, true);
                Assert.Fail("Expect Argument Exception, none thrown");
            }
            catch (ArgumentException e)
            {
                e.Should().BeOfType<ArgumentException>();
                Assert.Pass();
            }
        }

        [Test]
        public void Saves_cookies_prefs()
        {
            cookieService.SaveCookiesPreferences(new SaveCookiePreferenceModel());
        }

        [Test]
        public void Should_return_valid_cookie_pref_model()
        {
            httpContext.Request.Cookies = new Mock<IRequestCookieCollection>().Object;
            httpContext.Session = new Mock<ISession>().Object;
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            cookieService = new CookieService(_cookieNamesOptions, _httpContextAccessor.Object);

            var result = cookieService.GetUserCookiePreferences();
            result.Should().BeOfType<UserCookiePreferencesModel>();
        }
    }
}