using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenQA.Selenium.Internal;

namespace Beis.LearningPlatform.Web.Tests.ControllerTests;

public class CookiesControllerTests
{
    private CookiesController _controller;
    private Mock<ICookieControllerHelper> _helper;

    [SetUp]
    public void Setup()
    {
        _helper = new Mock<ICookieControllerHelper>();
        _controller = new CookiesController(_helper.Object);
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        var response = _controller.ControllerContext.HttpContext.Response;
    }

    [Test]
    public async Task Should_return_view_with_valid_model()
    {
        UserCookiePreferencesModel payload = new UserCookiePreferencesModel()
        {
            IsCookieBannerClosed = false,
        };
        _helper.Setup(x => x.GetUserCookiePreferences())
            .Returns(new ControllerHelperOperationResponse<UserCookiePreferencesModel>(new Guid(), payload));
        _helper.Setup(x => x.ProcessGetCustomPageResult("Custom-pages/cookies"))
            .Returns(Task.FromResult(new CMSPageViewModel()));
        var result = await _controller.Cookies();
        result.Should().BeOfType<ViewResult>();
    }

    [Test]
    public async Task Should_return_bad_request_if_no_cookie_preferences()
    {
        _helper
            .Setup(x => x.GetUserCookiePreferences())
            .Returns(new ControllerHelperOperationResponse<UserCookiePreferencesModel>(new Guid(), false, ""));
        
        var result = await _controller.Cookies();
        result.Should().BeOfType<BadRequestResult>();
    }

    [Test]
    public void Should_process_valid_cookie_request_and_redirect_valid_referrer()
    {
        _helper.Setup(x => x.ProcessCookie("close", true)).Returns(new ControllerHelperOperationResponse(new Guid()));
        string redirectUrl = "/";
        _helper.Setup(x => x.SafeRedirectToReferer(out redirectUrl)).Returns(true);
        var result = _controller.ProcessCookie("home", null, "close", true);
        result.Should().BeOfType<RedirectResult>();
    }
    
    [Test]
    public void Should_process_valid_cookie_request_and_redirect_invalid_referrer()
    {
        _helper.Setup(x => x.ProcessCookie("close", true)).Returns(new ControllerHelperOperationResponse(new Guid()));
        string redirectUrl = "/";
        _helper.Setup(x => x.SafeRedirectToReferer(out redirectUrl)).Returns(false);
        var result = _controller.ProcessCookie(null, null, "close", true);
        result.Should().BeOfType<RedirectToActionResult>();
    }
    
    [Test]
    public void Should_return_bad_request_if_processing_cookie_fails()
    {
        _helper
            .Setup(x => x.ProcessCookie("close", true))
            .Returns(new ControllerHelperOperationResponse(new Guid(), false));
        
        var result = _controller.ProcessCookie(null, null, "close", true);
        
        result.Should().BeOfType<BadRequestResult>();
    }

    [Test]
    public void Should_return_bad_request_if_cookie_prefer_save_fails()
    {
        SaveCookiePreferenceModel model = new SaveCookiePreferenceModel();
        _helper.Setup(x => x.SaveCookiesPreferences(model))
            .Returns(new ControllerHelperOperationResponse(new Guid(), false));
        var result = _controller.SaveCookiesPreferences(model);
        result.Should().BeOfType<BadRequestResult>();
    }
    
    
    [Test]
    public void Should_return_redirect_if_cookie_prefer_save_succeeds()
    {
        SaveCookiePreferenceModel model = new SaveCookiePreferenceModel();
        _helper.Setup(x => x.SaveCookiesPreferences(model))
            .Returns(new ControllerHelperOperationResponse(new Guid(), true));
        var result = _controller.SaveCookiesPreferences(model);
        result.Should().BeOfType<RedirectToActionResult>();
    }
    


}