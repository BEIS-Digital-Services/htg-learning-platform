namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class SatisfactionSurveyControllerHelper : ControllerHelperBase, ISatisfactionSurveyControllerHelper
    {
        private readonly ISatisfactionSurveyService _satisfactionSurveyService;
        private readonly IMapper _mapper;

        public SatisfactionSurveyControllerHelper(ILogger<ControllerHelperBase> logger,
            ISatisfactionSurveyService satisfactionSurveyService,
            IMapper mapper) : base(logger)
        {
            _satisfactionSurveyService = satisfactionSurveyService;
            _mapper = mapper;
        }

        public async Task<ControllerHelperOperationResponse> SaveSatisfactionSurvey(SatisfactionSurveyViewModel surveyForm)
        {
            var requestId = RecordRequest();
            var traceMsg = $"{nameof(SaveSatisfactionSurvey)} ({requestId})";
            _logger.LogTrace(traceMsg);

            try
            {
                var satisfactionSurveyDto = _mapper.Map<SatisfactionSurveyDto>(surveyForm);
                var result = await _satisfactionSurveyService.SaveSatisfactionSurvey(requestId, satisfactionSurveyDto);
                if (!result.IsValid)
                {
                    throw new ApplicationException(result.Message ?? traceMsg);
                }

                return new ControllerHelperOperationResponse(requestId, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, traceMsg);
                return new ControllerHelperOperationResponse(requestId, false, "Failed to save survey");
            }

        }
    }
}