namespace Beis.LearningPlatform.Web.Models
{
    public class CmsAccordionItemViewModel
    {
        public string Header { get; set; }
        public string copy1 { get; set; }
        public string copy2 { get; set; }

        public bool HasContent
        {
            get
            {
                return !string.IsNullOrEmpty(Header)
                    && !string.IsNullOrEmpty(copy1)
                    && !string.IsNullOrEmpty(copy2);
            }
        }

    }
}
