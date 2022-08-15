namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsTextWithArrowImageViewComponentTests : BaseViewComponentTest
    {
        [Test]
        public void Invoke_WhenCalledWithIncompleteData_ShouldNotHaveContent()
        {

            var component = new CmsTextWithArrowImageViewComponent();

            var cmsPageComponent = new CMSPageComponent{};
            var view = component.Invoke(cmsPageComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);
            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Invoke_WhenCalledWithCompleteData_ShouldHaveContent()
        {

            var component = new CmsTextWithArrowImageViewComponent();
            var cmsPageComponent = new CMSPageComponent
            {
                text = "test"
            };
            var view = component.Invoke(cmsPageComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Component);
            Assert.IsTrue(model.HasContent);
            Assert.AreEqual(model.Component.text, cmsPageComponent.text);
        }
        private static ViewDataDictionary<CmsTextWithArrowImageViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsTextWithArrowImageViewModel>;
            return viewComponentData;
        }
    }
}
