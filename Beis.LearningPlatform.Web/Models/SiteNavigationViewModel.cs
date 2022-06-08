namespace Beis.LearningPlatform.Web.Models
{
    public class SiteNavigationViewModel
    {
		private readonly IPageViewModel _pageViewModel;
		private readonly PathString? _requestPath;

		public SiteNavigationViewModel(IPageViewModel pageViewModel, PathString? requestPath)
        {            
            _pageViewModel = pageViewModel ?? throw new ArgumentNullException(nameof(pageViewModel));
			_requestPath = requestPath ?? throw new ArgumentNullException(nameof(requestPath));
        }

        public IEnumerable<SiteNavigationModel> SiteNavigationModels { get; set; }

		internal void SetActiveNavigationModel()
		{
			foreach (var siteNavigationModel in SiteNavigationModels)
			{
				siteNavigationModel.SetActiveNavigationModel(_pageViewModel, _requestPath.Value);
			}
		}
	}
}