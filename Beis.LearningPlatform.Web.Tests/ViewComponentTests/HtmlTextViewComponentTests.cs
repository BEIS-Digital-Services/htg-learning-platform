using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Services;
using Beis.LearningPlatform.Web.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NUnit.Framework;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class HtmlTextViewComponentTests : BaseViewComponentTest
    {
        private IHtmlTextService _htmlTextService;

        [SetUp]
        public void Setup()
        {
            this._htmlTextService = new HtmlTextService();

        }

        private HtmlTextViewComponent CreateViewComponent()
        {
            return new HtmlTextViewComponent(_htmlTextService);
        }


        [Test]
        public void Should_Return_Valid_View_And_Data()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new HtmlTextInputViewModel { Text = "Some Text" });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);
            
            Assert.AreEqual(model.HtmlText, "Some Text");
        }

        private static ViewDataDictionary<HtmlTextViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<HtmlTextViewModel>;
            return viewComponentData;
        }
    }
}