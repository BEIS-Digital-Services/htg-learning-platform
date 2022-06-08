using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class CmsBusinessCardComponentTests : BaseViewComponentTest
    {
        private const string ApiBaseUrl = "https://ApiBaseUrl";
        private const string ImageUrl = "/imageurl.jpg";
        private const string BusinessName = "BusinessName Ltd";
        private readonly IOptions<CmsOption> _cmsOptions = ConfigOptions.Create(new CmsOption { ApiBaseUrl = ApiBaseUrl });

        private CmsBusinessCardViewComponent CreateViewComponent()
        {
            return new CmsBusinessCardViewComponent(_cmsOptions);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_Image_And_No_BusinessName()
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
        public void Should_Not_Have_Content_If_No_Image()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
                businessName = BusinessName
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Not_Have_Content_If_No_BusinessName()
        {
            var component = CreateViewComponent();
            var view = component.Invoke(new CMSPageComponent
            {
                image = new CMSPageImage
                {
                    url = ImageUrl
                }
            });

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsFalse(model.HasContent);
        }

        [Test]
        public void Should_Have_Content_If_BusinessName_And_Image()
        {

            var component = CreateViewComponent();
            var view = component.Invoke(GetValidCmsPageComponent());

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
            var view = component.Invoke(GetValidCmsPageComponent());

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.HasContent);

            Assert.IsNotNull(model.ImageUrl);
            
            Assert.IsTrue(model.ImageUrl.StartsWith(ApiBaseUrl));
            Assert.IsTrue(model.ImageUrl.EndsWith(ImageUrl));
        }


        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("www.website.com")]
        [TestCase("http://www.website.com")]
        [TestCase("https://www.website.com")]
        public void Should_Have_Valid_Visit_Url_If_Visit_Set(string visitUrl)
        {
            var component = CreateViewComponent();
            var cmsPageComponent = GetValidCmsPageComponent();
            cmsPageComponent.visit = visitUrl;
            var view = component.Invoke(cmsPageComponent);

            var viewComponentData = GetViewComponentData(view);
            Assert.IsNotNull(viewComponentData);

            var model = viewComponentData.Model;
            Assert.IsNotNull(model);

            if (string.IsNullOrWhiteSpace(visitUrl))
            {
                Assert.IsTrue(string.IsNullOrWhiteSpace(model.VisitUrl));
            }
            else
            { 
                Assert.IsTrue(model.VisitUrl.StartsWith("http") || model.VisitUrl.StartsWith("//"));
            }            
        }

        private static ViewDataDictionary<CmsBusinessCardViewModel> GetViewComponentData(IViewComponentResult view)
        {
            var viewComponentResult = view as ViewViewComponentResult;
            var viewComponentData = viewComponentResult.ViewData as ViewDataDictionary<CmsBusinessCardViewModel>;
            return viewComponentData;
        }

        private static CMSPageComponent GetValidCmsPageComponent()
        {
            return new CMSPageComponent
            {
                businessName = BusinessName,
                image = new CMSPageImage
                {
                    url = ImageUrl
                }
            };
        }
    }
}