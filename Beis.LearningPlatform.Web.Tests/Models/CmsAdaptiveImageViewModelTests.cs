namespace Beis.LearningPlatform.Web.Tests.Models
{
    public class CmsAdaptiveImageViewModelTests
    {
        const string BaseUrl = "base url";

        [Test]
        public void Test_Constructor_Throws_IfNoCmsPageComponent()
        {
            Assert.Throws<ArgumentNullException>(() => { new CmsAdaptiveImageViewModel(null, BaseUrl); });
        }

        [Test]
        public void Test_Constructor_Throws_IfBaseUrlIsEmpty()
        {
            var cmsPageComponent = new CMSPageComponent();

            Assert.Throws<ArgumentException>(() => { new CmsAdaptiveImageViewModel(cmsPageComponent, ""); });
        }

        [Test]
        public void Test_Component_ReturnsCmsPageComponent()
        {
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.Component, Is.EqualTo(cmsPageComponent));
        }

        [Test]
        public void Test_BaseUrl_ReturnsBaseUrl()
        {
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.BaseUrl, Is.EqualTo(BaseUrl));
        }

        [Test]
        public void Test_CssClassTopSpace_ReturnsEmpty_IfComponentTopSpace_IsNoSpace()
        {
            var cmsPageComponent = new CMSPageComponent
            {
                topSpace = "nospace"
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.CssClassTopSpace, Is.EqualTo(String.Empty));
        }

        [Test]
        public void Test_CssClassTopSpace_ReturnsCss_IfComponentTopSpace_IsNotNoSpace()
        {
            var cmsPageComponent = new CMSPageComponent
            {
                topSpace = "anything"
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.CssClassTopSpace, Is.EqualTo(" govuk-!-margin-top-6"));
        }

        [Test]
        public void Test_CssClassBottomSpace_ReturnsEmpty_IfComponentBottomSpace_IsNoSpace()
        {
            var cmsPageComponent = new CMSPageComponent
            {
                bottomSpace = "nospace"
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.CssClassBottomSpace, Is.EqualTo(String.Empty));
        }

        [Test]
        public void Test_CssClassBottomSpace_ReturnsCss_IfComponentBottomSpace_IsNotNoSpace()
        {
            var cmsPageComponent = new CMSPageComponent
            {
                bottomSpace = "anything"
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.CssClassBottomSpace, Is.EqualTo(" govuk-!-margin-bottom-6"));
        }

        [Test]
        public void Test_DesktopImageUrl_ReturnsNull_IfNoDesktopImage()
        {
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.DesktopImageUrl, Is.Null);
        }

        [Test]
        public void Test_DesktopImageUrl_ReturnsNull_IfDesktopImageHasNoContent()
        {
            var cmsPageComponent = new CMSPageComponent
            {
                desktop_image = new CMSPageImage()
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.DesktopImageUrl, Is.Null);
        }

        [Test]
        public void Test_DesktopImageUrl_ReturnsImageUrl_IfDesktopImageHasContent()
        {
            var imageUrl = "image url";
            var cmsPageComponent = new CMSPageComponent
            {
                desktop_image = new CMSPageImage
                {
                    url = imageUrl
                }
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.DesktopImageUrl, Is.EqualTo($"{BaseUrl}{imageUrl}"));
        }

        [Test]
        public void Test_MobileImageUrl_ReturnsNull_IfNoMobileImage()
        {
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.MobileImageUrl, Is.Null);
        }

        [Test]
        public void Test_MobileImageUrl_ReturnsNull_IfMobileImageHasNoContent()
        {
            var cmsPageComponent = new CMSPageComponent
            {
                mobile_image = new CMSPageImage()
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.MobileImageUrl, Is.Null);
        }

        [Test]
        public void Test_MobileImageUrl_ReturnsImageUrl_IfMobileImageHasContent()
        {
            var imageUrl = "image url";
            var cmsPageComponent = new CMSPageComponent
            {
                mobile_image = new CMSPageImage
                {
                    url = imageUrl
                }
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.MobileImageUrl, Is.EqualTo($"{BaseUrl}{imageUrl}"));
        }

        [Test]
        public void Test_HasContent_ReturnsFalse_IfNoImages()
        {
            var cmsPageComponent = new CMSPageComponent();

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.HasContent, Is.False);
        }

        [Test]
        public void Test_HasContent_ReturnsFalse_IfNoDesktopImage()
        {
            var imageUrl = "image url";
            var cmsPageComponent = new CMSPageComponent
            {
                mobile_image = new CMSPageImage
                {
                    url = imageUrl
                }
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.HasContent, Is.False);
        }

        [Test]
        public void Test_HasContent_ReturnsFalse_IfNoMobileImage()
        {
            var imageUrl = "image url";
            var cmsPageComponent = new CMSPageComponent
            {
                desktop_image = new CMSPageImage
                {
                    url = imageUrl
                }
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.HasContent, Is.False);
        }

        [Test]
        public void Test_HasContent_ReturnsTrue_IfBothDesktopAndMobileImage()
        {
            var imageUrl = "image url";
            var cmsPageComponent = new CMSPageComponent
            {
                desktop_image = new CMSPageImage
                {
                    url = imageUrl
                },
                mobile_image = new CMSPageImage
                {
                    url = imageUrl
                }
            };

            var test = new CmsAdaptiveImageViewModel(cmsPageComponent, BaseUrl);

            Assert.That(test.HasContent, Is.True);
        }
    }
}