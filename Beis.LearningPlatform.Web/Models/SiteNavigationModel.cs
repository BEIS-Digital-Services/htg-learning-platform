using Beis.LearningPlatform.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Beis.LearningPlatform.Web.Models
{
	public class SiteNavigationModel
	{
        public SiteNavigationItemModel NavigationItem { get; set; }
        public IEnumerable<SiteNavigationItemModel> SubNavigationItems { get; set; }

		internal void SetActiveNavigationModel(IPageViewModel pageViewModel, PathString path)
		{
			foreach (var subNavigationItem in SubNavigationItems)
			{
				if (subNavigationItem.SetActiveNavigationModel(pageViewModel, path))
				{
					this.NavigationItem.IsActive = true; // If subnav item is active - this is also active.
					return;
				}
			}

			this.NavigationItem.SetActiveNavigationModel(pageViewModel, path);
		}

		
		public bool ShowSubMenu
		{
			get {
				return (this.NavigationItem?.IsActive ?? false)
					&& (this.SubNavigationItems?.Any() ?? false);
			}
		}
	}
}
