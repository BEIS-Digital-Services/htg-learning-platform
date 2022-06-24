namespace Beis.LearningPlatform.BL.Services
{
    public class SatisfactionSurveyService : ISatisfactionSurveyService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISatisfactionSurveyDataService _satisfactionSurveyDataService;

        public SatisfactionSurveyService(ILogger<SatisfactionSurveyService> logger,
                            IMapper mapper,
                            ISatisfactionSurveyDataService satisfactionSurveyDataService)
        {
            _logger = logger;
            _mapper = mapper;
            _satisfactionSurveyDataService = satisfactionSurveyDataService;
        }

        public async Task<IServiceResponse<int>> SaveSatisfactionSurvey(Guid requestId, SatisfactionSurveyDto satisfactionSurveyDto)
        {
            if (satisfactionSurveyDto == null)
            {
                throw new ArgumentNullException(nameof(satisfactionSurveyDto));
            }

            var rtnValue = await _satisfactionSurveyDataService.Add(satisfactionSurveyDto);
            return new ServiceResponse<int>(requestId, rtnValue != default, nameof(satisfactionSurveyDto), rtnValue);
        }


    }
}