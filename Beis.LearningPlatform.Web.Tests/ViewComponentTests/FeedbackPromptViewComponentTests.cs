namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests;

public class FeedbackPromptViewComponentTests : BaseViewComponentTest
{
    private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
    private readonly Mock<ICookieService> _cookieService = new();
    private readonly Mock<HttpContext> _httpContext = new();
    private Mock<HttpRequest> _request = new();
    private Mock<IQueryCollection> _queryCollection = new();

    [SetUp]
    public void Setup()
    {
        _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(_httpContext.Object);
        _httpContext.SetupGet(x => x.Request).Returns(_request.Object);
    }

    [Test]
    public void Invoke_ShouldHaveCmsComponentContent()
    {

        var component = new FeedbackPromptViewComponent(_cookieService.Object, _httpContextAccessor.Object);

        var cmsPageComponent = new CMSPageComponent
        {
            message = "Test Header"
        };
        var view = component.Invoke(cmsPageComponent);

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);
        Assert.AreEqual(cmsPageComponent.message, model.CmsPageComponent.message);
    }

    [Test]
    public void Invoke_WhenJavascriptDisabled_ShouldReturn_ViewModelWithDisabled()
    {        
        _cookieService.Setup(x => x.GetUserCookiePreferences()).Returns(new UserCookiePreferencesModel { 
            IsJavascriptEnabled = false
        });

        var component = new FeedbackPromptViewComponent(_cookieService.Object, _httpContextAccessor.Object);


        var cmsPageComponent = new CMSPageComponent
        {
            header = "test"
        };
        var view = component.Invoke(cmsPageComponent);

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);
        Assert.IsFalse(model.IsJavascriptEnabled);
    }


    
    [Test]
    public void Invoke_NoFormSubmitted_ShouldReturn_ViewModelWithNoFormSubmitted()
    {
        _request.SetupGet(x => x.Query).Returns(_queryCollection.Object);
        var component = new FeedbackPromptViewComponent(_cookieService.Object, _httpContextAccessor.Object);


        var cmsPageComponent = new CMSPageComponent
        {
            header = "test"
        };
        var view = component.Invoke(cmsPageComponent);

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);
        Assert.IsFalse(model.IsFeedbackSubmitted);
    }



    private static ViewDataDictionary<FeedbackPromptViewModel> GetViewComponentData(IViewComponentResult view)
    {
        var viewComponentResult = view as ViewViewComponentResult;
        var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<FeedbackPromptViewModel>;
        return viewComponentData;
    }
}
