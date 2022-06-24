namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSSearchArticlePicker
    {

        public int id { get; set; }
        public CMSSearchArticle SearchArticle { get; set; }

        /// <summary>
        /// Note these are reduced objects from a picker, we do not have the full article details here.
        /// </summary>
        public bool HasContent
        {
            get 
            {
                return !string.IsNullOrEmpty(SearchArticle?.articleType);
            }
        }

        
    }
}