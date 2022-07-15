namespace Beis.LearningPlatform.Web.Models
{
    public class FeedbackPromptViewModel
    {
        public bool? IsJavascriptEnabled { get; set; }
        public bool IsFeedbackSubmitted { get; set; }
        public CMSPageComponent CmsPageComponent { get; set; }        
    }
}