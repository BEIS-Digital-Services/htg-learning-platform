using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NUnit.Framework;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
	public class CmsLandingPageHeroComponentTests : BaseViewComponentTest
    {
        private const string TestData_ImageUrl = "/imageurl.jpg";
        private const string TestData_Header = "Header";
		private const string TestData_Intro = "Intro";
		private const string TestData_LinkUrl = "LinkUrl";
		private const string TestData_LinkText = "LinkText";

        private CmsLandingPageHeroViewComponent CreateViewComponent()
        {
            return new CmsLandingPageHeroViewComponent();
        }

        private CmsLandingPageHeroViewModel GetValidViewModel()
        {
            return new CmsLandingPageHeroViewModel
            {
                    Header = TestData_Header,
					Intro = TestData_Intro,
					LinkUrl = TestData_LinkUrl,
					LinkText = TestData_LinkText,
                    Image = new CMSPageImage { 
                        url = TestData_ImageUrl
                    }
            };
        }

        [Test]
        public void Should_Not_Have_Content_If_NoViewModelData()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CmsLandingPageHeroViewModel { });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Image()
        {
            var component = CreateViewComponent();
            
            var invalidViewModel = GetValidViewModel();
            invalidViewModel.Image = null;

            var view = component.Invoke(invalidViewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Link_If_No_Link()
        {
            var component = CreateViewComponent();
            
            var invalidViewModel = GetValidViewModel();
            invalidViewModel.LinkText = string.Empty;
            invalidViewModel.LinkUrl = null;

            var view = component.Invoke(invalidViewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasLink);
        }

        [Test]
        public void Should_Have_Content_If_ValidModel()
        {
            var component = CreateViewComponent();
            
            var validViewModel = GetValidViewModel();
            var view = component.Invoke(validViewModel);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);
        }

   

        [Test]
        public void Should_Have_Valid_Image_Url_If_Has_Content()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(GetValidViewModel());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.IsNotNull(model.Image?.url);            
            Assert.AreEqual(TestData_ImageUrl, model.Image?.url);
        }



        private static ViewDataDictionary<CmsLandingPageHeroViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsLandingPageHeroViewModel>;
            return viewComponentData;
        }

    }
}