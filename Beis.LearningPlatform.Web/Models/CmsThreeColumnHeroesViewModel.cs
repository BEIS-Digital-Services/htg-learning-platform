namespace Beis.LearningPlatform.Web.Models
{
    public class CmsThreeColumnHeroesViewModel
    {
        private readonly CMSPageComponent _cmsPageComponent;

        public CmsThreeColumnHeroesViewModel(CMSPageComponent cmsPageComponent)
        {
            _cmsPageComponent = cmsPageComponent ?? throw new ArgumentNullException(nameof(cmsPageComponent));
        }

        public bool HasContent
        {
            get
            {
                return _cmsPageComponent.ColumnOne?.HasContent == true
                    && _cmsPageComponent.ColumnTwo?.HasContent == true
                    && _cmsPageComponent.ColumnThree?.HasContent == true;
            }
        }

        public IEnumerable<CMSColumnHero> Columns
        {
            get
            {
                return new CMSColumnHero[] { _cmsPageComponent.ColumnOne, _cmsPageComponent.ColumnTwo, _cmsPageComponent.ColumnThree };
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