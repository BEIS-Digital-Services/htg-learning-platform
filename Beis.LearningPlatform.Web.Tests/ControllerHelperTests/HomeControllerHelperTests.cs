using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests
{
    public class HomeControllerHelperTests
    {
        private IHomeControllerHelper _homeControllerHelper;

        private Mock<ILogger<HomeControllerHelper>> _logger;
        private Mock<ICmsService> _cmsService;
        private CmsFeedbackService _cmsFeedbackService;
        private IOptions<CmsOption> _cmsOptions;
        
        private Mock<IFeedbackService> _feedbackService;
        private Mock<IHttpContextAccessor> _httpContextAccessor;


        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<HomeControllerHelper>>();
            _cmsService = new Mock<ICmsService>();
            _feedbackService = new Mock<IFeedbackService>();

            _cmsFeedbackService = new CmsFeedbackService(_feedbackService.Object);
            _cmsOptions = ConfigOptions.Create(new CmsOption());
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _homeControllerHelper = new HomeControllerHelper(_logger.Object, _cmsService.Object, _cmsFeedbackService, _cmsOptions,
                  _httpContextAccessor.Object);
        }

        [Test]
        public void Should_return_empty_tags_for_empty_tags_passed()
        {
            var result = _homeControllerHelper.GetCurrentTags(null);
            result.Should().BeEmpty();
        }

        [Test]
        public void Should_return_valid_tag_list_for_valid_tags_passed()
        {
            var result = _homeControllerHelper.GetCurrentTags("tag1,tag2,tag3");
            result.Should().NotBeEmpty();
            result.Count.Should().Be(3);
        }

        [Test]
        public void Should_return_valid_distinct_tag_list_for_valid_tags_passed()
        {
            var result = _homeControllerHelper.GetCurrentTags("tag1,tag1,tag1");
            result.Should().NotBeEmpty();
            result.Count.Should().Be(1);
        }

        [Test]
        public void Should_return_empty_tags_for_empty_tags_passed1()
        {
            var result = _homeControllerHelper.GetCurrentTags(null, null, true);
            result.Should().BeEmpty();
        }


        [Test]
        public void Should_return_valid_tags_with_a_tag_removed()
        {
            var result = _homeControllerHelper.GetCurrentTags("tag1,tag2,tag3", "tag1", true);
            result.Should().NotBeEmpty();
            result.Count.Should().Be(2);
            result.Any(tag => tag == "tag1").Should().BeFalse();
        }

        [Test]
        public void Should_return_valid_tags_with_a_tag_is_not_removed()
        {
            var result = _homeControllerHelper.GetCurrentTags("tag1,tag2,tag3", "tag1", false);
            result.Should().NotBeEmpty();
            result.Count.Should().Be(3);
            result.Any(tag => tag == "tag1").Should().BeTrue();
        }

        [Test]
        public void Should_return_valid_tags_with_a_new_tag_is_added()
        {
            var result = _homeControllerHelper.GetCurrentTags("tag1,tag2,tag3", "tag4", false);
            result.Should().NotBeEmpty();
            result.Count.Should().Be(4);
            result.Any(tag => tag == "tag4").Should().BeTrue();
        }

        [Test]
        public async Task Should_set_reactive_components()
        {
            var component = new CMSPageComponent()
            {
                __component = "page.reactive-tags"
            };

            var pagename = "my-page-name";
            var model = new CMSPageViewModel { pagename = pagename, 
                components = new List<CMSPageComponent> {
                   
                } };

            model.components.Add(component);

            var articles = new List<CMSSearchArticle>();
            articles.Add(new CMSSearchArticle { id =1 });
            _cmsService.Setup(cms => cms.GetSearchArticlesResult(It.IsAny<string>())).ReturnsAsync(articles);

            await _homeControllerHelper.SetReactiveTagComponents(model);
            var componentToTest = model.components.First();
            componentToTest.Should().NotBeNull();

            componentToTest.search_articles.Should().NotBeEmpty();
        }
    }
}
