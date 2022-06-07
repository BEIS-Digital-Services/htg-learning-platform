namespace Beis.LearningPlatform.BL.Services
{
    public interface ISatisfactionSurveyService
	{
		Task<IServiceResponse<int>> SaveSatisfactionSurvey(Guid requestID, SatisfactionSurveyDto satisfactionSurveyDto);
	}
}