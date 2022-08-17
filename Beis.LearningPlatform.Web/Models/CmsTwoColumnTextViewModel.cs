namespace Beis.LearningPlatform.Web.Models;

public class CmsTwoColumnTextViewModel
{
    private readonly CMSPageComponent _cmsPageComponent;
    public CmsTwoColumnTextViewModel(CMSPageComponent cmsPageComponent)
    {
        _cmsPageComponent = cmsPageComponent;
    }

    public bool HasContent
    {
        get
        {
            return !string.IsNullOrEmpty(_cmsPageComponent.copy1)
                && !string.IsNullOrEmpty(_cmsPageComponent.copy2)
                && !string.IsNullOrEmpty(_cmsPageComponent.DividerText);
        }
    }

    public CMSPageComponent Component
    {
        get
        {
            return _cmsPageComponent;
        }
    }
}
