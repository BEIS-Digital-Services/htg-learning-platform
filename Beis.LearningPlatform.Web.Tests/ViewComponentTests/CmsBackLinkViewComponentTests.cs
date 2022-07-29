namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsBackLinkViewComponentTests : BaseViewComponentTest
    {
        [SetUp]
        public void Setup() { }

        private CmsBackLinkViewComponent CreateViewComponent()
        {
            return new CmsBackLinkViewComponent();
        }


        [Test]
        public void Should_Have_Default_Content_If_No_Link()
        {
            var pagename = "my-page-name";
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageViewModel { pagename = pagename }, new CMSPageComponent()); 

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.AreEqual("Back", model.BackLink.LinkText);
            Assert.AreEqual("javascript:", model.BackLink.LinkUrl);
            Assert.AreEqual($"back-link-{pagename}", model.LinkId);
            Assert.IsTrue(model.IsJavascriptLink);
        }

        [Test]
        public void Should_Have_Content_If_ValidColumns()
        {
            var pagename = "my-page-name";
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageViewModel { pagename = pagename }, new CMSPageComponent
            {
                BackLink = new CMSSimpleLink {
                     LinkText = "LinkText", LinkUrl = "LinkUrl"
                }
            }); 

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.AreEqual("LinkText", model.BackLink.LinkText);
            Assert.AreEqual("LinkUrl", model.BackLink.LinkUrl);
            Assert.AreEqual($"back-link-{pagename}", model.LinkId);
            Assert.IsFalse(model.IsJavascriptLink);
        }


        private static ViewDataDictionary<CmsBackLinkViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsBackLinkViewModel>;
            return viewComponentData;
        }
    }
}