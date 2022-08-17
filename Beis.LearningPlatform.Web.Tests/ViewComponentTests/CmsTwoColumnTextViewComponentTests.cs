namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsTwoColumnTextViewComponentTests : BaseViewComponentTest
    {
        [Test]
        public void Invoke_WhenCalledWithIncompleteData_ShouldNotHaveContent()
        {

            var component = new CmsTwoColumnTextViewComponent();

            var cmsPageComponent = new CMSPageComponent
            {
                copy1 = "test"
            };
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

            var component = new CmsTwoColumnTextViewComponent();
            var cmsPageComponent = new CMSPageComponent
            {
                copy1 = "test",
                copy2 = "test",
                DividerText = "OR"
            };
            var view = component.Invoke(cmsPageComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Component);
            Assert.IsTrue(model.HasContent);
            Assert.AreEqual(model.Component.copy1, cmsPageComponent.copy1);
            Assert.AreEqual(model.Component.copy2, cmsPageComponent.copy2);
            Assert.AreEqual(model.Component.DividerText, cmsPageComponent.DividerText);
        }
        private static ViewDataDictionary<CmsTwoColumnTextViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsTwoColumnTextViewModel>;
            return viewComponentData;
        }
    }
}
