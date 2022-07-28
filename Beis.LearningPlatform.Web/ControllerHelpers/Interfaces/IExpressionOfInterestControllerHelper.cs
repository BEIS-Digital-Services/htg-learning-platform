namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface IExpressionOfInterestControllerHelper
    {
        Task<HttpStatusCode> SaveExpressionOfInterest(ExpressionOfInterestDto expressionOfInterestDto);
    }
}