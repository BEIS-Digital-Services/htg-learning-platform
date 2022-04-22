using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.Tests.MockClasses;
using NUnit.Framework;

namespace Beis.LearningPlatform.Web.Tests.ClassTests
{
    public class CMSClassesTests
    {
        private StrapiMakeApiCallMockService _MakeApiCallService;

        [SetUp]
        public void Setup()
        {
            _MakeApiCallService = new StrapiMakeApiCallMockService();
        }

        [Test]
        public void Test_CMSSearchTag()
        {
            CMSSearchTag tag = new CMSSearchTag();
            tag.hide = true;
            Assert.NotNull(tag);
        }


        [Test]
        public void Test_CMSPageSideNavigation()
        {
            CMSPageSideNavigation sideNav = new CMSPageSideNavigation();
            sideNav.hide = true;
            Assert.NotNull(sideNav);
        }

        [Test]
        public void Test_CMSPageArticles()
        {
            CMSPageArticles articles = new CMSPageArticles();
            articles.__component = null;
            articles.type = null;
            articles.Type = null;
            articles.Column1 = null;
            articles.Column2 = null;
            articles.Column3 = null;
            articles.Links1 = null;
            articles.Links2 = null;
            articles.Links3 = null;
            articles.Tags1 = null;
            articles.Tags2 = null;
            articles.Tags3 = null;
            articles.Image1 = null;
            articles.Image2 = null;
            articles.Image3 = null;
            Assert.NotNull(articles);
        }
    }
}