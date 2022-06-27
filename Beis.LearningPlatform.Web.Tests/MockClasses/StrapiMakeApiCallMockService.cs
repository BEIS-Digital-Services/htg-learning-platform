namespace Beis.LearningPlatform.Web.Tests.MockClasses
{
    public class StrapiMakeApiCallMockService : IMakeApiCallService
    {
        private string _StrapiMockJson = "C:\\Data\\BEIS-Veracity\\beis-help-to-grow-web-app\\Web.Tests\\sources\\CustomPagesStrapiMock.json";
        private string _StrapiSearchArticlesMockJson = "C:\\Data\\BEIS-Veracity\\beis-help-to-grow-web-app\\Web.Tests\\sources\\SearchArticlesStrapiMock.json";
        public async Task<string> GetApiResult(string Baseurl, string strapiAction)
        {
            if(strapiAction.Contains("search-articles"))
            {
                _StrapiMockJson = _StrapiSearchArticlesMockJson;
            }

            string ReturnViewModel = "";
            if (File.Exists(_StrapiMockJson))
            {
                if (strapiAction.Contains("Custom-pages"))
                {
                    string source = File.ReadAllText(_StrapiMockJson);
                    var objectList = string.IsNullOrWhiteSpace(source) ? new List<CMSPageViewModel>() : JsonConvert.DeserializeObject<List<CMSPageViewModel>>(source);

                    var customPage = objectList.Find(item => item.pagename == strapiAction.Replace("Custom-pages/", ""));
                    ReturnViewModel = customPage == null ? "" : JsonConvert.SerializeObject(customPage);
                }
                if (strapiAction.Contains("search-articles"))
                {
                    string source = File.ReadAllText(_StrapiMockJson);
                    var objectList = string.IsNullOrWhiteSpace(source) ? new List<CMSSearchArticle>() : JsonConvert.DeserializeObject<List<CMSSearchArticle>>(source);

                    ReturnViewModel = objectList == null ? "" : JsonConvert.SerializeObject(objectList);

                }
            }
            else
            {
                throw new FileNotFoundException("The CustomPagesStrapiMockJson file can not be found in path " + _StrapiMockJson);
            }

            return await Task.FromResult(ReturnViewModel);
        }
        public string StrapiMockJsonFilePath { get { return _StrapiMockJson; } set { _StrapiMockJson = value; } }
    }
}