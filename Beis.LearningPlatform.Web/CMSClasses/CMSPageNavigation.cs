using System;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageNavigation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public bool searchbar { get; set; }
        public DateTime published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int? custom_page { get; set; }
        public IList<CMSPageLink> links { get; set; }

        public bool? hide { get; set; }
    }
}