using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NUnit.Framework;
using System.Linq;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsThreeColumnHeroesViewComponentTests : BaseViewComponentTest
    {
        [SetUp]
        public void Setup() { }

        private CmsThreeColumnHeroesViewComponent CreateViewComponent()
        {
            return new CmsThreeColumnHeroesViewComponent();
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Columns()
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
        public void Should_Not_Have_Content_If_InvalidColumns()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
                ColumnOne = new CMSColumnHero(),
                ColumnTwo = new CMSColumnHero(),
                ColumnThree = null
            }); 

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Have_Content_If_ValidColumns()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
                ColumnOne = GetValidCmsColumnHero(),
                ColumnTwo = GetValidCmsColumnHero(),
                ColumnThree = GetValidCmsColumnHero(),
            }); 

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.AreEqual(3, model.Columns.Count());
        }


        private static ViewDataDictionary<CmsThreeColumnHeroesViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsThreeColumnHeroesViewModel>;
            return viewComponentData;
        }

        private static CMSColumnHero GetValidCmsColumnHero()
        {
            return new CMSColumnHero() { Header = "Header", Intro = "Intro", LinkText = "LinkText", LinkUrl = "LinkUrl" };
        }
    }
}