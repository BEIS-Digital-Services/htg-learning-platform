using Beis.LearningPlatform.Web.StrapiApi.Models;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Models
{
    public class PartialNavigationModel
    {
        public PartialNavigationModel() { }
        public PartialNavigationModel(string pageName, IEnumerable<CMSPageNavigation> navigation) 
        {
            this.PageName = pageName;
            this.Navigation = navigation;
        }
        public string PageName { get; set; }
        public IEnumerable<CMSPageNavigation> Navigation { get; set; }
    }
}