namespace Beis.LearningPlatform.Web.ViewComponents;

public class CmsImageItemViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CmsImageItemViewComponent(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
    {
        var viewModel = new CmsImageItemViewModel(cmsPageComponent);
        return View(viewModel);
    }
}