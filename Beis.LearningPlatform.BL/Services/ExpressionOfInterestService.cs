namespace Beis.LearningPlatform.BL.Services
{
    public class ExpressionOfInterestService : IExpressionOfInterestService
    {
        private readonly IExpressionOfInterestDataService _expressionOfInterestDataService;

        public ExpressionOfInterestService(IExpressionOfInterestDataService expressionOfInterestDataService)
        {
            _expressionOfInterestDataService = expressionOfInterestDataService;
        }

        public async Task<IServiceResponse<int>> SaveExpressionOfInterest(Guid requestId, ExpressionOfInterestDto expressionOfInterestDto)
        {
            var entityId = await _expressionOfInterestDataService.Add(expressionOfInterestDto);
            return new ServiceResponse<int>(requestId, entityId != default, string.Empty, entityId);
        }
    }
}