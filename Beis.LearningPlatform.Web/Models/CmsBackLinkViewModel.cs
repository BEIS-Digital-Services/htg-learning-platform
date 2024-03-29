﻿namespace Beis.LearningPlatform.Web.Models
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
                return !string.IsNullOrEmpty(LinkId);
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
                return null;
            }
        }

        public bool IsJavascriptLink
        {
            get
            {
                return _cmsPageComponent.BackLink?.HasContent != true;
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