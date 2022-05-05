using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace Beis.LearningPlatform.Web.Models
{
	public class SiteNavigationItemModel
	{
		/// <summary>
		/// Id from the CMS item
		/// </summary>
		public int id { get; set; }
		/// <summary>
		/// "Label text" for the link
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Order by
		/// </summary>
		public int Order { get; set; }
		/// <summary>
		/// If no CMS page chosen, the Url we are linking to
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		/// Used to determine if the navigation item is selected
		/// </summary>
		public string UrlRegEx { get; set; }

		internal bool SetActiveNavigationModel(IPageViewModel pageViewModel, PathString path)
		{
			if (pageViewModel != null && this.PageViewModel?.id == pageViewModel.id)
			{
				this.IsActive = true;
			}

			if (this.PageViewModel == null && !string.IsNullOrEmpty(this.UrlRegEx))
			{
				this.IsActive = new Regex(this.UrlRegEx).IsMatch(path);
			}
			
			return this.IsActive;
		}

		/// <summary>
		/// Optionally apply a custom css class
		/// </summary>
		public string CssClass { get; set; }
		/// <summary>
		/// Aria text (optional, we can render title)
		/// </summary>
		public string AriaText { get; set; }

		/// <summary>
		/// The CMS page linked to.
		/// </summary>
		[JsonProperty("custom_page")]
		public CMSPageViewModel PageViewModel { get; set; }

		/// <summary>
		/// True of the item links to the current page 
		/// </summary>
		public bool IsActive { get; set; }

		public string GetMenuUrl()
		{
			if (this.PageViewModel != null)
			{
				return $"/{this.PageViewModel.pagename}";
			}
			return this.Url ?? "/#navmissingurl";
		}

		public string GetGaLinkId()
		{
			return $"{this.Title}-{this.id}".UrlEncode(true);
		}

		public string GetAriaLabel(string prefix)
		{
			return this.AriaText ?? $"{prefix} {this.Title}";
		}
	}
}
