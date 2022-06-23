namespace Beis.LearningPlatform.Web.Models;

public class CmsImageItemViewModel
{
    private readonly CMSPageComponent _cmsPageComponent;
    private readonly bool _applyCompletedLink;

    public CmsImageItemViewModel(CMSPageComponent cmsPageComponent, bool applyCompletedLink)
    {
        _cmsPageComponent = cmsPageComponent;
        _applyCompletedLink = applyCompletedLink;
    }

    public bool HasContent
    {
        get
        {
            return !string.IsNullOrEmpty(_cmsPageComponent.header)
                && !string.IsNullOrEmpty(_cmsPageComponent.Summary)
                && _cmsPageComponent.image != null
                && _cmsPageComponent.Link != null;
        }
    }

    public string Header
    {
        get
        {
            return _cmsPageComponent.header;
        }
    }

    public string Summary
    {
        get
        {
            return _cmsPageComponent.Summary;
        }
    }

    public CMSPageImage Image
    {
        get
        {
            return _cmsPageComponent.image;
        }
    }

    public CMSPageLink Link
    {
        get
        {
            if (_applyCompletedLink && _cmsPageComponent.CompletedLink != null)
                return _cmsPageComponent.CompletedLink;
            else
                return _cmsPageComponent.Link;
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