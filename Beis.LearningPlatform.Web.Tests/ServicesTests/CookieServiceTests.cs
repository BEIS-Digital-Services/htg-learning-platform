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
            _cmsLogger = new();
            _Configuration = new Mock<IConfiguration>();
            _CmsService = new Mock<ICmsService>();
            _FeedbackService = new Mock<IFeedbackService>();
            _CookieService = new Mock<ICookieService>();
            _MakeApiCallService = new StrapiMakeApiCallMockService();
            _httpContextAccessor = new();

            cmsService = new CmsService(_cmsLogger.Object, _cmsOptions, _MakeApiCallService);
            cookieService = new CookieService(_cookieNamesOptions, _httpContextAccessor.Object);

            _TempData = new Mock<ITempDataDictionary>();
        }
    }
}