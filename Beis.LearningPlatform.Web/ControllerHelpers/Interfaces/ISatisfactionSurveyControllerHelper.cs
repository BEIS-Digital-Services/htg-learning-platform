namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface ISatisfactionSurveyControllerHelper
    {
        Task<ControllerHelperOperationResponse> SaveSatisfactionSurvey(SatisfactionSurveyViewModel surveyForm);
    }
}