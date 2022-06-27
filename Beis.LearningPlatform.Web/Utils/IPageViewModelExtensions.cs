namespace Beis.LearningPlatform.Web.Utils
{
    public static class IPageViewModelExtensions
    {
        public static bool ShowLayoutNavigation(this IPageViewModel pageViewModel)
        {
            return pageViewModel.navigations?.Any() ?? false;
        }


        [Obsolete("Non-CMS pages should pull content and title from CMS using a key (backlogged tech-debt item)")]
        /// <summary>
        /// For non CMS pages, allows setting page title.
        /// </summary>        
        public static void SetPageTitle(this IPageViewModel viewModel, string pageTitle)
        {
            viewModel.pageTitle = pageTitle;
        }

        /// <summary>
        /// Used for pages in main top navigation
        /// </summary>
        public static void SetPageNameForNavigation(this IPageViewModel viewModel, string pageName)
        {
            viewModel.pagename = pageName;
        }

        /// <summary>
        /// Default the index/follow properties to true if not set in the CMS (i.e. default is for search bots to index and follow, but this can optionally be disabled).
        /// </summary>
        public static string GetRobotsMetaTag(this IPageViewModel viewModel)
        {
            var index = viewModel.index ?? true;
            var follow = viewModel.follow ?? true;
            return $"{(index ? "index" : "noindex")}, {(follow ? "follow" : "nofollow")}";
        }

        public static PartialNavigationModel GetPartialNavigationModel(this IPageViewModel viewModel)
        {
            if (viewModel.navigations == null)
            {
                return null;
            }

            return new PartialNavigationModel(viewModel.pagename, viewModel.navigations);
        }
    }
}