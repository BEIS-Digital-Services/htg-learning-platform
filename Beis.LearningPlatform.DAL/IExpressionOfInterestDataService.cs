namespace Beis.LearningPlatform.DAL
{
    public interface IExpressionOfInterestDataService
    {
        Task<int> Add(ExpressionOfInterestDto expressionOfInterestDto);
    }
}