namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsNextPageRadioButtonViewComponentTests : BaseViewComponentTest
    {
        [Test]
        public void Invoke_WhenCalledWithIncompleteData_ShouldNotHaveContent()
        {

            var component = new CmsNextPageRadioButtonViewComponent();

            //all urls are missing
            var cmsPageComponent = new CMSPageComponent
            {
                Radio1Text = "radio1",
                Radio2Text = "radio2",
                Radio3Text = "radio2",
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

            var component = new CmsNextPageRadioButtonViewComponent();
            var cmsPageComponent = new CMSPageComponent
            {
                Radio1BoldLeadText = "b1",
                Radio2BoldLeadText = "b2",
                Radio3BoldLeadText = "b3",
                Radio1Text = "radio1",
                Radio2Text = "radio2",
                Radio3Text = "radio2",
                Radio1Url = "/1",
                Radio2Url = "/2",
                Radio3Url = "/3",
                ButtonText = "Continue"

            };
            var view = component.Invoke(cmsPageComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Component);
            Assert.IsTrue(model.HasContent);
            Assert.AreEqual(model.Radio1BoldLeadText, cmsPageComponent.Radio1BoldLeadText);
            Assert.AreEqual(model.Radio2BoldLeadText, cmsPageComponent.Radio2BoldLeadText);
            Assert.AreEqual(model.Radio3BoldLeadText, cmsPageComponent.Radio3BoldLeadText);
            Assert.AreEqual(model.Radio1Text, cmsPageComponent.Radio1Text);
            Assert.AreEqual(model.Radio2Text, cmsPageComponent.Radio2Text);
            Assert.AreEqual(model.Radio3Text, cmsPageComponent.Radio3Text);
            Assert.AreEqual(model.Radio1Url, cmsPageComponent.Radio1Url);
            Assert.AreEqual(model.Radio2Url, cmsPageComponent.Radio2Url);
            Assert.AreEqual(model.Radio3Url, cmsPageComponent.Radio3Url);
            Assert.AreEqual(model.ButtonText, cmsPageComponent.ButtonText);
        }
        private static ViewDataDictionary<CmsNextPageRadioButtonViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsNextPageRadioButtonViewModel>;
            return viewComponentData;
        }
    }
}
