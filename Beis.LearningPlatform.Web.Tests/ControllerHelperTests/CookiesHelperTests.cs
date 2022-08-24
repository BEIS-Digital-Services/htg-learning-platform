namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests;

public class CookiesHelperTests
{
    private CookieControllerHelper _helper;
    private Mock<ICookieService> _cookieService;
    private Mock<ILogger<HomeControllerHelper>> _logger;
    private Mock<ICmsService> _cmsService;
    private Mock<ICmsFeedbackService> _cmsFeedbackService;
    private Mock<IOptions<CmsOption>> _cmsOptions;
    private Mock<IHttpContextAccessor> _httpContextAccessor;
    private Mock<HttpContext> _httpContext;
    private Mock<HttpRequest> _httpRequest;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<HomeControllerHelper>>();
        _cookieService = new Mock<ICookieService>();
        _cmsService = new Mock<ICmsService>();
        _cmsFeedbackService = new Mock<ICmsFeedbackService>();
        _cmsOptions = new Mock<IOptions<CmsOption>>();
        _httpContextAccessor = new Mock<IHttpContextAccessor>();
        _httpContext = new Mock<HttpContext>();
        _httpRequest = new Mock<HttpRequest>();
        _helper = new CookieControllerHelper(
            _logger.Object, _cmsService.Object, _cmsFeedbackService.Object, _cookieService.Object, _cmsOptions.Object, _httpContextAccessor.Object);
    }

    [Test]
    public void Should_return_request_id_on_process_request()
    {
        var result = _helper.ProcessCookie(String.Empty, false);
        result.RequestID.Should().NotBeEmpty();
    }
    
    [Test]
    public void Should_return_request_id_on_save_cookies_request()
    {
        var result = _helper.SaveCookiesPreferences(new SaveCookiePreferenceModel());
        result.RequestID.Should().NotBeEmpty();
    }
    
    [Test]
    public void Should_return_valid_cookies_model()
    {
        _cookieService.Setup(x => x.GetUserCookiePreferences()).Returns(new UserCookiePreferencesModel());
        var result = _helper.GetUserCookiePreferences();
        result.Payload.Should().BeOfType<UserCookiePreferencesModel>();
    }
    
    [Test]
    public void Should_return_not_redirect_to_external_url()
    {
        var refererUrl = "https://www.notourdomain.com";

        _httpContextAccessor.SetupGet(x => x.HttpContext)
            .Returns(_httpContext.Object);
        _httpContext.SetupGet(x => x.Request)
            .Returns(_httpRequest.Object);
        _httpRequest.SetupGet(x => x.Headers)
            .Returns(new HeaderDictionary { { "Referer", refererUrl } });

        string redirectUrl;
        var result = _helper.SafeRedirectToReferer(out redirectUrl);
        result.Should().Be(true);
        redirectUrl.Should().Be("/");

    }
    
    [Test]
    public void Should_return_redirect_to_internal()
    {
        var refererUrl = "https://www.learn-to-grow-your-business.service.gov.uk/cookies";

        _httpContextAccessor.SetupGet(x => x.HttpContext)
            .Returns(_httpContext.Object);
        _httpContext.SetupGet(x => x.Request)
            .Returns(_httpRequest.Object);
        _httpRequest.SetupGet(x => x.Headers)
            .Returns(new HeaderDictionary { { "Referer", refererUrl } });

        string redirectUrl;
        var result = _helper.SafeRedirectToReferer(out redirectUrl);
        result.Should().Be(true);
        redirectUrl.Should().Be("/cookies");

    }
    
}   