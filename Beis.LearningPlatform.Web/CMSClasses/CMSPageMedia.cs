namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageMedia
    {

            public int id { get; set; }
            public string name { get; set; }
            public string alternativeText { get; set; }
            public string caption { get; set; }
            public int? width { get; set; }
            public int? height { get; set; }
            public object formats { get; set; }
            public string hash { get; set; }
            public string ext { get; set; }
            public string mime { get; set; }
            public double size { get; set; }
            public string url { get; set; }
            public object previewUrl { get; set; }
            public string provider { get; set; }
            public object provider_metadata { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
    }
}