namespace Beis.LearningPlatform.Web.ControllerHelpers
{

    public class ExpressionOfInterestControllerHelper : ControllerHelperBase, IExpressionOfInterestControllerHelper
    {
        private readonly IExpressionOfInterestService _expressionOfInterestService;

        public ExpressionOfInterestControllerHelper(ILogger<FeedbackControllerHelper> logger, IExpressionOfInterestService expressionOfInterestService) : base(logger)
        {
            _expressionOfInterestService = expressionOfInterestService;
        }


        public async Task<HttpStatusCode> SaveExpressionOfInterest(ExpressionOfInterestDto expressionOfInterestDto)
        {
            var result = await _expressionOfInterestService.SaveExpressionOfInterest(Guid.NewGuid(), expressionOfInterestDto);
            return result.IsValid ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

    }
}