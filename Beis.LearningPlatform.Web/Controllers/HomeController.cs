namespace Beis.LearningPlatform.Web.Controllers
{
    public class HomeController : CmsControllerBase
    {
        private readonly IHomeControllerHelper _homeControllerHelper;


        public HomeController(ILogger<HomeController> logger, IHomeControllerHelper homeControllerHelper) : base(logger)
        {
            _homeControllerHelper = homeControllerHelper;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _homeControllerHelper.ProcessGetCustomPageResult("Custom-pages/home");
            viewModel.SetPageNameForNavigation("Home");
            return View(viewModel);
        }

        [HttpGet("Home/Diagnostic_Tool")]
        public IActionResult Diagnostic_Tool()
        {
            return RedirectToAction("Start", "DiagnosticTool");
        }

        [HttpGet("Home/Comparison_Tool")]
        public IActionResult Comparison_Tool()
        {
            return RedirectToAction("Start", "ComparisonTool");
        }

        [HttpGet("Home/Comparison_ToolNoJs")]
        public IActionResult Comparison_ToolNoJs()
        {
            return RedirectToAction("StartNoJs", "ComparisonTool");
        }

        [Route("/guidance-and-tools/{tag?}")]
        public async Task<IActionResult> Guidance_and_tools([FromRoute] string tag, [FromQuery] bool? previewSearchArticles)
        {
            if (!string.IsNullOrWhiteSpace(tag))
            { 
                return RedirectToTaggedArticleList(tag);
            }

            var viewModel = await _homeControllerHelper.ProcessGetCustomPageResult("Custom-pages/guidance-and-tools");
            viewModel.PreviewSearchArticles = previewSearchArticles;

            await _homeControllerHelper.SetReactiveTagComponents(viewModel);
            viewModel.SetPageNameForNavigation("Guidance and Tools");
            return View("Resources", viewModel);
        }

        [Route("/guidance-and-tools/update-tags/{tag}/{tagAction}")]
        public IActionResult GuidanceAndToolsUpdateTags(string tag, string tagAction, [FromQuery] string yourTags)
        {
            return RedirectToTaggedArticleList(tag, tagAction, yourTags);
        }

        private IActionResult RedirectToTaggedArticleList(string tag, string tagAction = null, string yourTags = null)
        {
            var currentTags = _homeControllerHelper.GetCurrentTags(yourTags, tag, tagAction == "remove");
            return currentTags.Any() ?
                RedirectToAction(nameof(GuidanceAndToolsShowTags), new { yourTags = string.Join(',', currentTags) }) : // Querystring is state, redirect with all
                RedirectToAction(nameof(Guidance_and_tools));
        }

        [Route("/guidance-and-tools/tags")]
        public async Task<IActionResult> GuidanceAndToolsShowTags([FromQuery] string yourTags)
        {
            var currentTags = _homeControllerHelper.GetCurrentTags(yourTags);
            if (!currentTags.Any())
            {
                return RedirectToAction("Guidance_and_tools");
            }

            var viewModel = await _homeControllerHelper.ProcessFilterCustomPageResultByTags(currentTags);
            viewModel.SetPageNameForNavigation("Guidance and Tools");
            return View("Resources", viewModel);
        }

        [Route("/help-and-support")]
        public async Task<IActionResult> Help_and_support()
        {
            var viewModel = await _homeControllerHelper.ProcessGetCustomPageResult("Custom-pages/help");
            viewModel.SetPageTitle("Help to Grow: Digital - Help and Support");
            return View("Help", viewModel);
        }

        [Route("/privacy")]
        public async Task<IActionResult> Privacy()
        {
            var viewModel = await _homeControllerHelper.ProcessGetCustomPageResult("Custom-pages/privacy");
            viewModel.SetPageTitle("Help to Grow: Digital - Privacy");
            viewModel.ShowBackButton = true;
            return View("Privacy", viewModel);
        }



        [Route("/accessibility-statement")]
        public async Task<IActionResult> Accessibility_Statement()
        {
            var viewModel = await _homeControllerHelper.ProcessGetCustomPageResult("Custom-pages/accessibility-statement");
            viewModel.SetPageTitle("Help to Grow: Digital - Accessibility Statement");
            viewModel.ShowBackButton = true;
            return View("Privacy", viewModel);
        }


        [Route("/about")]
        public async Task<IActionResult> About()
        {
            var viewModel = await _homeControllerHelper.ProcessGetCustomPageResult("Custom-pages/about");
            viewModel.SetPageNameForNavigation("About");
            return View("About", viewModel);
        }

        [Route("/about2")]
        public async Task<IActionResult> about2()
        {
            var viewModel = await _homeControllerHelper.ProcessGetCustomPageResult("sn-pages/about");
            viewModel.SetPageNameForNavigation("About");
            return View("Sidenav", viewModel);
        }

        /// <summary>
        /// WARNING: For Custom-Pages only
        /// This method handles dynamic content routing from strapi. 
        /// Routes such as those found in the Header Navs have explicit actions defined
        /// </summary>
        /// <param name="strapiAction">the Custom-Pages strapi action</param>
        /// <returns></returns>
        [Route("/{strapiAction}")]
        public async Task<IActionResult> CMSCustomPagesRoute(string strapiAction)
        {
            var viewModel = await _homeControllerHelper.ProcessGetCustomPageResult("Custom-pages/" + strapiAction);
            if (viewModel.id == default)
            {
                if (!string.IsNullOrEmpty(viewModel.RedirectTo))
                    return RedirectPermanent(viewModel.RedirectTo);
                return GetNotFoundPageResult();
            }
            return GetStrapiCustomPageView(strapiAction, viewModel);
        }

        [Route("preview-content/{strapiAction}")]
        public async Task<IActionResult> CMSPreviewCustomPage(string strapiAction)
		{
			var viewModel = await _homeControllerHelper.ProcessGetCustomPageResult("Custom-pages/preview/" + strapiAction);
			return GetStrapiCustomPageView(strapiAction, viewModel);
		}

		private IActionResult GetStrapiCustomPageView(string strapiAction, CMSPageViewModel viewModel)
		{
			if (strapiAction == "terms-and-conditions" || strapiAction == "get-in-touch" || strapiAction == "cookies")
			{
                viewModel.ShowBackButton = true;
            }
            return View("Resources", viewModel);
		}

		private IActionResult GetNotFoundPageResult()
		{            
            var viewModel = new ErrorViewModel { title = "Page not found", description = "We could not find the page you requested", RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
			return View("Error", viewModel);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}