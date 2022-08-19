namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests;

public class CmsImageViewComponentTests : BaseViewComponentTest
{
    private readonly Mock<IOptions<CmsOption>> _cmsOptions = new();
    private readonly string _apiBaseUrl = "https://localhost/";

    [SetUp]
    public void Setup()
    {
        _cmsOptions.SetupGet(x => x.Value).Returns(new CmsOption { ApiBaseUrl = _apiBaseUrl } );
    }

    [Test]
    public void Invoke_WhenCalledWithCompleteData_ShouldHaveContent()
    {
        var component = new CmsImageViewComponent(_cmsOptions.Object);
        var viewModel = new CmsImageViewModel
        {
            ImageUrl = "ImageUrl",
            CssClass = "CssClass",
            CssStyle = "CssStyle",
            Height = 90,
            Width = 160,
        };
        var view = component.Invoke(viewModel);

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);
        Assert.IsTrue(model.IsValid);
        Assert.AreEqual($"{_apiBaseUrl}ImageUrl", model.FullImageUrl);
    }

    [Test]
    public void Invoke_WhenCalledWithIncompleteData_ShouldNotHaveContent()
    {

          var component = new CmsImageViewComponent(_cmsOptions.Object);


        var viewModel = new CmsImageViewModel
        {
            CssClass = "CssClass",
            CssStyle = "CssStyle",
            Height = 90,
            Width = 160,
        };
        var view = component.Invoke(viewModel);

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);
        Assert.IsFalse(model.IsValid);
    }

    [Test]
    public void Invoke_TitleShouldDefaultToEmptyString()
    {
        var component = new CmsImageViewComponent(_cmsOptions.Object);
        var viewModel = new CmsImageViewModel
        {
            ImageUrl = "ImageUrl",
            CssClass = "CssClass",
            CssStyle = "CssStyle",
            Height = 90,
            Width = 160,
        };
        var view = component.Invoke(viewModel);

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);
        Assert.IsTrue(model.IsValid);
        Assert.IsNotNull(model.Title);
        Assert.AreEqual("", model.Title);
    }



    private static ViewDataDictionary<CmsImageViewModel> GetViewComponentData(IViewComponentResult view)
    {
        var viewComponentResult = view as ViewViewComponentResult;
        var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsImageViewModel>;
        return viewComponentData;
    }
}
