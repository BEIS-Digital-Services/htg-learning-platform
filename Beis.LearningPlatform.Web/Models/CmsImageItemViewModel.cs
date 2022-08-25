namespace Beis.LearningPlatform.Web.Models;

public class CmsImageItemViewModel
{
    private readonly CMSPageComponent _cmsPageComponent;

    public CmsImageItemViewModel(CMSPageComponent cmsPageComponent)
    {
        _cmsPageComponent = cmsPageComponent;
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

    public CMSPageLink CompletedLink
    {
        get
        {
            if (_cmsPageComponent.CompletedLink != null)
                return _cmsPageComponent.CompletedLink;
            else
                return _cmsPageComponent.Link;
        }
    }
    public string UniqueActionName
    {
        get
        {
            return _cmsPageComponent.UniqueActionName;
        }
    }

    public string UniqueActionNameWithId
    {
        get
        {
            return $"{_cmsPageComponent.UniqueActionName}_{_cmsPageComponent.id}";
        }
    }

    public string UniqueIdJS { get; set; }
}