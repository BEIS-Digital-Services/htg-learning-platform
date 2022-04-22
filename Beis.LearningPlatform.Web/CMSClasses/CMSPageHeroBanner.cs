using System;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageHeroBanner
    {
        public int id { get; set; }
        public string HeroImageLocation { get; set; }
        public DateTime published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string name { get; set; }
        public string leftBackgroundColor { get; set; }
        public string rightBackgroundColor { get; set; }
        public CMSPageHeroContent HeroContent { get; set; }
        public CMSPageImage logo { get; set; }
        public CMSPageImage HeroImage { get; set; }
    }
}