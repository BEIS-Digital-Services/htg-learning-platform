using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsSocialMediaViewComponentTests : BaseViewComponentTest
    {
        private const string Facebook = "https://facebook.com";
        private const string Twitter = "https://twitter.com";
        private const string InstaGram = "https://instagram.com";
        private const string LinkedIn = "https://linkedin.com";
        private const string ShareLink = "https://share.facebook.com";
        private const string ShareLinkTitle = "this is title";
        private const string Path = "/the-benefits-of-digital-to-your-business";
        private const string PageTitle = "Help to Grow: Digital - the benefits of digital to your business";
        private const string PageTitleUrlEncoded = "Help+to+Grow%3a+Digital+-+the+benefits+of+digital+to+your+business";
        private const string BaseUrl = "https://www.learn-to-grow-your-business.service.gov.uk";

        private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
        private readonly Mock<HttpContext> _httpContext = new();
        private readonly Mock<HttpRequest> _httpRequest = new();
        private readonly Mock<IPageViewModel> _pageViewModel = new();
        private readonly IOptions<WebsiteOption> _websiteOptions = ConfigOptions.Create(new WebsiteOption { BaseUrl = BaseUrl });

        [SetUp]
        public void Setup()
        {
            _httpRequest.SetupGet(x => x.Path)
                .Returns(Path);
            _httpContext.SetupGet(x => x.Request)
                .Returns(_httpRequest.Object);
            _httpContextAccessor.SetupGet(x => x.HttpContext)
               .Returns(_httpContext.Object);
            _pageViewModel.SetupGet(x => x.pageTitle)
                .Returns(PageTitle);
        }

        private CmsSocialMediaViewComponent CreateViewComponent()
        {
            return new CmsSocialMediaViewComponent(_httpContextAccessor.Object, _websiteOptions);
        }

        [Test]
        public void Should_Have_Facebook_Link()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(_pageViewModel.Object, new CMSPageComponent
            {
                facebook = Facebook
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsNotNull(model.Component.facebook);
        }

        [Test]
        public void Should_Have_Twitter_Link()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(_pageViewModel.Object, new CMSPageComponent
            {
                twitter = Twitter
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsNotNull(model.Component.twitter);
        }

        [Test]
        public void Should_Have_Instagram_Link()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(_pageViewModel.Object, new CMSPageComponent
            {
                instagram = InstaGram
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsNotNull(model.Component.instagram);
        }

        [Test]
        public void Should_Have_LinkedIn_Link()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(_pageViewModel.Object, new CMSPageComponent
            {
                linkedIn = LinkedIn
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsNotNull(model.Component.linkedIn);
        }

        [Test]
        public void Should_Have_Share_Link_And_Title()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(_pageViewModel.Object, new CMSPageComponent
            {
                shareLink = ShareLink,
                shareLinkTitle = ShareLinkTitle
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsNotNull(model.Component.shareLink);
            Assert.IsNotNull(model.Component.shareLinkTitle);
        }

        [Test]
        public void Should_Provide_Default_Page_Title()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(_pageViewModel.Object, new CMSPageComponent{ });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsNotNull(model.ShareTitle);
            Assert.AreEqual(model.ShareTitle, PageTitleUrlEncoded);
        }

        [Test]
        public void Should_Provide_Default_Page_Url()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(_pageViewModel.Object, new CMSPageComponent{ });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsNotNull(model.ShareLink);
            Assert.AreEqual(model.ShareLink, string.Concat(BaseUrl, Path));
        }


        private static ViewDataDictionary<CmsSocialMediaViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsSocialMediaViewModel>;
            return viewComponentData;
        }
    }
}