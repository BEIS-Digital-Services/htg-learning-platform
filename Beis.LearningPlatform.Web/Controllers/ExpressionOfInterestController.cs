namespace Beis.LearningPlatform.Web.Controllers
{
    public class ExpressionOfInterestController : ControllerBase
    {
        private readonly IExpressionOfInterestControllerHelper _expressionOfInterestControllerHelper;

        public ExpressionOfInterestController(ILogger<ExpressionOfInterestController> logger, IExpressionOfInterestControllerHelper expressionOfInterestControllerHelper) : base(logger)
        {
            _expressionOfInterestControllerHelper = expressionOfInterestControllerHelper;
        }

        [HttpPost]
        [Route("/expression-of-interest", Name = "ExpressionOfInterest")]
        public async Task<JsonResult> ExpressionOfInterest([FromBody] ExpressionOfInterestDto expressionOfInterestDto)
        {
            if (!ModelState.IsValid)
            {
                return JsonBadRequest();
            }

            var statusCode = await _expressionOfInterestControllerHelper.SaveExpressionOfInterest(expressionOfInterestDto);
            return Json(statusCode);
        }
    }
}