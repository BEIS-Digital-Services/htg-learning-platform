namespace Beis.LearningPlatform.Web.Models
{
    public class CmsLinkListHeroViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsLinkListHeroViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get
            {
                return _cmsPageComponent.HeroLinks != null
                    && _cmsPageComponent.HeroLinks.Any(x => x != null && x.HasContent);
            }
        }

        public IList<CMSSimpleLink> HeroLinks
        {
            get
            {
                return _cmsPageComponent.HeroLinks;
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
}