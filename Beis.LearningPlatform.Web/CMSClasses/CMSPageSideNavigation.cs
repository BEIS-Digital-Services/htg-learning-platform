using System;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageSideNavigation
    {
        public int id { get; set; }
        public DateTime published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string name { get; set; }
        public IList<CMSPageSide> Side { get; set; }

        public bool? hide { get; set; }
    }
}