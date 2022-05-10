using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NUnit.Framework;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsThreeColumnHeroesComponentTests : BaseViewComponentTest
    {
        private const string TestData_ImageUrl = "/imageurl.jpg";
        private const string TestData_Header = "Header";
        private const string TestData_Intro = "Intro";
        private const string TestData_LinkUrl = "LinkUrl";
        private const string TestData_LinkText = "LinkText";

        private CmsThreeColumnHeroesViewComponent CreateViewComponent()
        {
            return new CmsThreeColumnHeroesViewComponent();
        }

        private static CMSPageComponent GetValidCmsComponent()
        {
            return new CMSPageComponent
            {
                ColumnOne = GetCMSColumnHero(1),
                ColumnTwo = GetCMSColumnHero(2),
                ColumnThree = GetCMSColumnHero(3)
            };
        }

        private static CMSColumnHero GetCMSColumnHero(int id)
        {
            return new CMSColumnHero
            {
                id = id,
                Header = $"Header{id}",
                Intro = $"Intro{id}",
                LinkText = $"LinkText{id}",
                LinkUrl = $"LinkUrl{id}"
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
        public void Should_Not_Have_Content_If_No_Heroes()
        {
            var component = CreateViewComponent();

            var invalidComponent = GetValidCmsComponent();
            invalidComponent.ColumnOne = null;
            invalidComponent.ColumnTwo = null;
            invalidComponent.ColumnThree = null;

            var view = component.Invoke(invalidComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }


        [TestCase("One")]
        [TestCase("Two")]
        [TestCase("Three")]
        [Test]
        public void Should_Not_Have_Content_If_MissingAnyHero(string nullPropertyName)
        {
            var component = CreateViewComponent();

            var invalidComponent = GetValidCmsComponent();
            switch (nullPropertyName)
            {
                case "One":
                    invalidComponent.ColumnOne = null;
                    break;
                case "Two":
                    invalidComponent.ColumnTwo = null;
                    break;
                case "Three":
                    invalidComponent.ColumnThree = null;
                    break;
                default:
                    break;
            }


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


        private static ViewDataDictionary<CmsThreeColumnHeroesViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsThreeColumnHeroesViewModel>;
            return viewComponentData;
        }

    }
}