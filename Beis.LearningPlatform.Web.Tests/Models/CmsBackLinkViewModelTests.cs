namespace Beis.LearningPlatform.Web.Tests.Models
{
    public class CmsBackLinkViewModelTests
    {
        [Test]
        public void Test_Constructor_Throws_IfNoCmsPageComponent()
        {
            var pageViewModel = new PageViewModel();

            Assert.Throws<ArgumentNullException>(() =>
                new CmsBackLinkViewModel(pageViewModel, null)
            );
        }

        [Test]
        public void Test_Constructor_Throws_IfNoPageViewModel()
        {
            var cmsPageComponent = new CMSPageComponent();

            Assert.Throws<NullReferenceException>(() =>
                new CmsBackLinkViewModel(null, cmsPageComponent)
            );
        }

        [Test]
        public void Test_HasContent_IsAlwaysTrue()
        {
            var pageViewModel = new PageViewModel();
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);

            Assert.That(test.HasContent, Is.True);
        }

        [Test]
        public void Test_BackLink_Null_IfCmsPageComponentBackLink_IsNull()
        {
            var pageViewModel = new PageViewModel();
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);

            Assert.That(test.BackLink, Is.Null);
        }

        [Test]
        public void Test_BackLink_Null_IfCmsPageComponentBackLink_HasContent_IsFalse()
        {
            var pageViewModel = new PageViewModel();
            var cmsPageComponent = new CMSPageComponent
            {
                BackLink = new CMSSimpleLink()
            };

            var test = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);

            Assert.That(test.BackLink, Is.Null);
        }

        [Test]
        public void Test_BackLink_Populated_IfCmsPageComponentBackLink_HasContent_IsTrue()
        {
            var pageViewModel = new PageViewModel();
            var cmsPageComponent = new CMSPageComponent
            {
                BackLink = new CMSSimpleLink
                {
                    LinkText = "link text",
                    LinkUrl = "link url"
                }
            };

            var test = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);

            Assert.That(test.BackLink, Is.EqualTo(cmsPageComponent.BackLink));
        }

        [Test]
        public void Test_IsJavascriptLink_True_IfCmsPageComponentBackLink_IsNull()
        {
            var pageViewModel = new PageViewModel();
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);

            Assert.That(test.IsJavascriptLink, Is.True);
        }

        [Test]
        public void Test_IsJavascriptLink_True_IfCmsPageComponentBackLink_HasContent_IsFalse()
        {
            var pageViewModel = new PageViewModel();
            var cmsPageComponent = new CMSPageComponent
            {
                BackLink = new CMSSimpleLink()
            };

            var test = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);

            Assert.That(test.IsJavascriptLink, Is.True);
        }

        [Test]
        public void Test_IsJavascriptLink_False_IfCmsPageComponentBackLink_HasContent_IsTrue()
        {
            var pageViewModel = new PageViewModel();
            var cmsPageComponent = new CMSPageComponent
            {
                BackLink = new CMSSimpleLink
                {
                    LinkText = "link text",
                    LinkUrl = "link url"
                }
            };

            var test = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);

            Assert.That(test.IsJavascriptLink, Is.False);
        }

        [Test]
        public void Test_LinkId_IsNeverEmpty()
        {
            var pageViewModel = new PageViewModel();
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);

            Assert.That(test.LinkId, Is.EqualTo("back-link-"));
        }

        [Test]
        public void Test_LinkId_IncludesPageName()
        {
            var pageViewModel = new PageViewModel
            {
                pagename = "page name"
            };
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsBackLinkViewModel(pageViewModel, cmsPageComponent);

            Assert.That(test.LinkId, Is.EqualTo($"back-link-{pageViewModel.pagename}"));
        }
    }
}