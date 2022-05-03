using Beis.LearningPlatform.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Models
{
	public class SiteNavigationViewModel
    {
		private readonly IPageViewModel _pageViewModel;

		public SiteNavigationViewModel(IPageViewModel pageViewModel)
        {
            
            _pageViewModel = pageViewModel ?? throw new ArgumentNullException(nameof(pageViewModel));
        }

        public IEnumerable<SiteNavigationModel> SiteNavigationModels { get; set; }

		internal void SetActiveNavigationModel(IPageViewModel pageViewModel, PathString path)
		{
			foreach (var siteNavigationModel in SiteNavigationModels)
			{
				siteNavigationModel.SetActiveNavigationModel(pageViewModel, path);
			}
		}
	}
}