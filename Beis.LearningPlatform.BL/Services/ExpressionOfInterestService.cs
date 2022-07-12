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
            if (expressionOfInterestDto == null)
            {
                throw new ArgumentNullException(nameof(expressionOfInterestDto));                
            }
                
            var returnValue = await _expressionOfInterestDataService.Add(expressionOfInterestDto);
            return new ServiceResponse<int>(requestId, true, string.Empty, returnValue);
        }
    }
}