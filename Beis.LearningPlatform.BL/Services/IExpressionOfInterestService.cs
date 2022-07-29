namespace Beis.LearningPlatform.BL.Services
{
    public interface IExpressionOfInterestService
    {
        Task<IServiceResponse<int>> SaveExpressionOfInterest(Guid requestId, ExpressionOfInterestDto expressionOfInterestDto);
    }
}