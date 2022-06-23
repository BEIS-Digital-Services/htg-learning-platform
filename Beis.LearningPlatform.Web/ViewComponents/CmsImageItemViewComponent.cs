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
        bool applyCompletedLink = false;
        string completedLinkSessionKey = $"{cmsPageComponent.UniqueActionName}__CompletedLink";
        if (!string.IsNullOrWhiteSpace(completedLinkSessionKey))
        {
            var completedLinkSessionValue = _httpContextAccessor.HttpContext.Session.GetString(completedLinkSessionKey);
            applyCompletedLink = completedLinkSessionValue == "true";
        }

        var viewModel = new CmsImageItemViewModel(cmsPageComponent, applyCompletedLink);
        return View(viewModel);
    }
}