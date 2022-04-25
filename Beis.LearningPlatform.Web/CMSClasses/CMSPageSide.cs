using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageSide
    {
        public int id { get; set; }
        public string name { get; set; }
        public IList<CMSPageLink> links { get; set; }

        public bool? hide { get; set; }
    }
}