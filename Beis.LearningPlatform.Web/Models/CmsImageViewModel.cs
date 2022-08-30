namespace Beis.LearningPlatform.Web.Models
{
    public class CmsImageViewModel
    {
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ImageUrl);
            }
        }

        public string FullImageUrl
        {
            get
            {
                return $"{BaseUrl}{ImageUrl}";
            }
        }

        public string BaseUrl { get; internal set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; } = "";
        public string CssClass { get; set; }
        public string CssStyle { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }        
    }
}