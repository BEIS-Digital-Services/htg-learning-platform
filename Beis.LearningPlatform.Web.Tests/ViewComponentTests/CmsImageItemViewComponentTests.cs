namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests;

public class CmsImageItemViewComponentTests : BaseViewComponentTest
{
    private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
    private readonly Mock<HttpContext> _httpContext = new();
    private Mock<ISession> _session = new();

    [SetUp]
    public void Setup()
    {
        _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(_httpContext.Object);
        _httpContext.SetupGet(x => x.Session).Returns(_session.Object);
    }

    [Test]
    public void Invoke_WhenCalledWithCompleteData_ShouldHaveContent()
    {

        var component = new CmsImageItemViewComponent(_httpContextAccessor.Object);


        var cmsPageComponent = new CMSPageComponent
        {
            header = "Test Header",
            Summary = "Test Summary",
            image = new CMSPageImage
            {
                url = "http://localhost/",
                alternativeText = "text"
            },
            Link = new CMSPageLink
            {
                label = "test link",
                url = "http://localhost/",
            },
            UniqueActionName = "test_action",
            CompletedLink = new CMSPageLink { }

        };
        var view = component.Invoke(cmsPageComponent);

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);
        Assert.IsTrue(model.HasContent);
        Assert.AreEqual(cmsPageComponent.header, model.Header);
        Assert.AreEqual(cmsPageComponent.Summary, model.Summary);
        Assert.AreEqual(cmsPageComponent.image, model.Image);
        Assert.AreEqual(cmsPageComponent.Link, model.Link);
    }

    [Test]
    public void Invoke_WhenCalledWithIncompleteData_ShouldNotHaveContent()
    {

        var component = new CmsImageItemViewComponent(_httpContextAccessor.Object);


        var cmsPageComponent = new CMSPageComponent
        {
            header = "test"
        };
        var view = component.Invoke(cmsPageComponent);

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);
        Assert.IsFalse(model.HasContent);
    }



    private static ViewDataDictionary<CmsImageItemViewModel> GetViewComponentData(IViewComponentResult view)
    {
        var viewComponentResult = view as ViewViewComponentResult;
        var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsImageItemViewModel>;
        return viewComponentData;
    }
}
