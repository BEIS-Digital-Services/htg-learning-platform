namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class DiagnosticToolEmailResponseHelper : IEmailResponseHelper
    {
        private readonly string _baseUrl;
        public DiagnosticToolEmailResponseHelper(IOptions<WebsiteOption> websiteOptions)
        {
            _baseUrl = websiteOptions.Value.BaseUrl;
            _baseUrl = _baseUrl.EndsWith("/") ? _baseUrl.TrimEnd('/') : _baseUrl;
        }

        public FormTypes FormType => FormTypes.DiagnosticTool;

        public async Task<IEmailDto> ConvertToResultsEmail(DiagnosticToolForm form)
        {
            var tradeMethod = form.steps[0].elements[0].GetSelectedAnswerOptionsAsString();
            var businessSector = form.steps[1].elements[0].selectedValue;
            var softwareNeeds = form.steps[2].elements[0].GetSelectedAnswerOptionsAsString();
            var question6Answer = form.steps[5].elements[0].value;
            var softwareChoices = form.steps[6].elements[0].GetSelectedAnswerOptionsAsString();
            var streamlineTasks = form.steps[7].elements[0].GetSelectedAnswerOptionsAsString();
            

            var tagOptions = form.steps[7].elements[0].GetSelectedAnswerOptions();
            var recommendedSoftware = await tagOptions.GetSearchTagDisplayNamesAsString();

            var returnValue = new DiagnosticToolResultsEmailDataDto()
            {
                BusinessSector = businessSector,
                Question6Answer = question6Answer,
                RecommendedSoftware = recommendedSoftware,
                SoftwareChoices = softwareChoices,
                SoftwareNeed = softwareNeeds,
                StreamlineTasks = streamlineTasks,
                TradeMethod = tradeMethod,
            };

            if (form.Articles?.Count > 0)
                returnValue.RelatedArticles = ConvertArticles(form.Articles);

            return returnValue;
        }

        private DiagnosticToolResultsEmailRelatedArticleDto ConvertArticle(StrapiApi.Models.CMSSearchArticle article)
        {
            var description = article.Article.copy;
            var title = string.IsNullOrWhiteSpace(article.Article.header) ? article.Article.subheader : article.Article.header;
            var url = _baseUrl;

            if (article.Links?.Count > 0)
                url += article.Links[0].url;

            title = title.TrimEnd('\n', '\r') + "\n";
            description = description.TrimEnd('\n', '\r') + "\n";

            return new()
            {
                ArticleURL = url,
                Description = description,
                Title = title
            };
        }

        private DiagnosticToolResultsEmailRelatedArticleDto[] ConvertArticles(IEnumerable<StrapiApi.Models.CMSSearchArticle> articles)
        {
            List<DiagnosticToolResultsEmailRelatedArticleDto> returnValue = new();

            if (articles != null)
                foreach (var article in articles)
                    returnValue.Add(ConvertArticle(article));

            return returnValue.ToArray();
        }
    }
}