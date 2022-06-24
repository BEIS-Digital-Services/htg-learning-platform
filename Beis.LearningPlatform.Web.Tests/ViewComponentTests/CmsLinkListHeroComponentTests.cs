namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsLinkListHeroComponentTests : BaseViewComponentTest
    {

        private CmsLinkListHeroViewComponent CreateViewComponent()
        {
            return new CmsLinkListHeroViewComponent();
        }

        private static CMSPageComponent GetValidCmsComponent()
        {
            return new CMSPageComponent
            {
                HeroLinks = new List<CMSSimpleLink>() {
                     new CMSSimpleLink{
                        id = 1,
                        LinkText = "LinkText1",
                        LinkUrl = "LinkUrl1"
                    },
                    new CMSSimpleLink{
                        id = 2,
                        LinkText = "LinkText2",
                        LinkUrl = "LinkUrl2"
                    }
                 }
            };
        }

        [Test]
        public void Should_Not_Have_Content_If_NoViewModelData()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent { });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Links()
        {
            var component = CreateViewComponent();

            var invalidComponent = GetValidCmsComponent();
            invalidComponent.HeroLinks = null;

            var view = component.Invoke(invalidComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Have_Content_If_ValidModel()
        {
            var component = CreateViewComponent();

            var validComponent = GetValidCmsComponent();
            var view = component.Invoke(validComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
        }


        private static ViewDataDictionary<CmsLinkListHeroViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsLinkListHeroViewModel>;
            return viewComponentData;
        }

    }
}