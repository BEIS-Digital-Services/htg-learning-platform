namespace Beis.LearningPlatform.Web.Interfaces
{
    public interface IMakeApiCallService
    {
        public Task<string> GetApiResult(string baseUrl, string strapiAction);
    }
}