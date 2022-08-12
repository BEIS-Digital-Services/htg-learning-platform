namespace Beis.LearningPlatform.Web.Models;

public class CmsTextWithArrowImageViewModel
{
    private readonly CMSPageComponent _cmsPageComponent;
    public CmsTextWithArrowImageViewModel(CMSPageComponent cmsPageComponent)
    {
        _cmsPageComponent = cmsPageComponent;
    }

    public bool HasContent
    {
        get
        {
            return !string.IsNullOrEmpty(_cmsPageComponent.text);
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
