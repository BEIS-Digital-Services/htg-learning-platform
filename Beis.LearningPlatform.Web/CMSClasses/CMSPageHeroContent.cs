using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageHeroContent
    {
        public int id { get; set; }
        public string name { get; set; }
        public string background { get; set; }
        public string type { get; set; }
        public IList<CMSPageTextblock> textblocks { get; set; }
        public IList<CMSPageButton> buttons { get; set; }
        public IList<CMSPageLink> links { get; set; }
        public CMSPageImage image { get; set; }
    }
}