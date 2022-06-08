namespace Beis.LearningPlatform.DAL
{
    public interface ISatisfactionSurveyDataService
    {
        Task<int> Add(SatisfactionSurveyDto satisfactionSurveyDto);
    }
}