namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests;

public class CmsHomePageWarningViewComponentTests : BaseViewComponentTest
{
    private MarkdownPipeline _markdownPipeline;

    [SetUp]
    public void Setup()
    {
        _markdownPipeline = GetMarkdownPipeline();
    }

    private CmsHomePageWarningViewComponent CreateViewComponent()
    {
        return new CmsHomePageWarningViewComponent(_markdownPipeline);
    }

    [Test]
    public void Should_Not_Have_Content_If_no_content()
    {
        var component = CreateViewComponent();
        var view = component.Invoke(new CMSPageComponent { });

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);

        Assert.IsFalse(model.HasContent);
    }

    [Test]
    public void Should_Convert_Markdown_If_Has_Content()
    {
        var component = CreateViewComponent();
        var view = component.Invoke(GetValidCmsPageComponent());

        var viewComponentData = GetViewComponentData(view);
        Assert.IsNotNull(viewComponentData);

        var model = viewComponentData.Model;
        Assert.IsNotNull(model);

        Assert.IsTrue(model.HasContent);

        Assert.IsNotNull(model.Header);
        Assert.IsNotNull(model.Intro);
        Assert.AreEqual("<p>intro</p>\n", model.IntroHtml);

    }

    private static ViewDataDictionary<CmsHomePageWarningViewModel> GetViewComponentData(IViewComponentResult view)
    {
        var viewComponentResult = view as ViewViewComponentResult;
        var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsHomePageWarningViewModel>;
        return viewComponentData;
    }

    private static CMSPageComponent GetValidCmsPageComponent()
    {
        return new CMSPageComponent
        {
            header = "header",
            intro = "intro"
        };
    }
}