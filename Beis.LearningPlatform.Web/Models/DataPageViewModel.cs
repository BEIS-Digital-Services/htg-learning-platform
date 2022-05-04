using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.Utils;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Models
{
    public class DataPageViewModel<T> : IPageViewModel where T : new()
    {
        private IPageViewModel _pageViewModel;
        public T Data { get; set; }

        public DataPageViewModel() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageViewModel"></param>
        /// <param name="data"></param>
        /// <param name="contentKey">Key that will uniquely identify the page content for GA purposes.</param>
        public DataPageViewModel(IPageViewModel pageViewModel, T data, string contentKey)
        {
            _pageViewModel = pageViewModel;
            this.pageTitle = _pageViewModel.pageTitle;
            this.pagename = _pageViewModel.pagename;
            this.ContentKey = contentKey;
            Data = data;
        }

        #region IPageViewModel implementation 

        public string pageTitle { get; set; }
        public string pagename { get; set; }
        public IList<CMSPageNavigation> navigations => _pageViewModel.navigations;
        public IList<CMSPageFooter> footers => _pageViewModel.footers;
        public IList<CMSPageSideNavigation> side_navigations => _pageViewModel.side_navigations;
        public string CmsBaseUrl { get; set; }
        public bool ShowLayoutNavigation => this.ShowLayoutNavigation();
        public PartialNavigationModel NavigationModel => this.GetPartialNavigationModel();

        internal void SetPageViewModel(IPageViewModel pageViewModel)
        {
            _pageViewModel = pageViewModel;
        }

        public string ContentKey { get; }

        // SEO properties:
        public string title { get { return _pageViewModel.title; } }
        public string description { get { return _pageViewModel.description; } }
        public string meta { get { return _pageViewModel.meta; } }
        public bool? index { get { return _pageViewModel.index; } }
        public bool? follow { get { return _pageViewModel.follow; } }

		public int id { get { return default; } }

		#endregion
	}
}