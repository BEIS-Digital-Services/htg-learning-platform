namespace Beis.LearningPlatform.Web.Models
{
    public class CmsBackLinkViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;
        private readonly string _linkId;

        public CmsBackLinkViewModel(IPageViewModel pageViewModel, CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
            _linkId = $"back-link-{pageViewModel.pagename}";
        }

        public bool HasContent
        {
            get
            {
                return BackLink.HasContent
                    && !string.IsNullOrEmpty(LinkId);
            }
        }

        public CMSSimpleLink BackLink
        {
            get
            {

                if (_cmsPageComponent.BackLink?.HasContent == true)
                {
                    return _cmsPageComponent.BackLink;
                }
                else
                {
                    return new CMSSimpleLink
                    {
                        LinkText = "Back",
                        LinkUrl = "javascript:history.go(-1);"
                    };
                }

            }
        }

        public string LinkId
        {
            get
            {
                return this._linkId;
            }
        }
    }
}