using Beis.LearningPlatform.Web.Interfaces;
using System;

namespace Beis.LearningPlatform.Web.Models
{
	public class SiteNavigationViewModel
    {
		private readonly IPageViewModel _pageViewModel;

		public SiteNavigationViewModel(IPageViewModel pageViewModel)
        {
            
            _pageViewModel = pageViewModel ?? throw new ArgumentNullException(nameof(pageViewModel));
        }

    }
}