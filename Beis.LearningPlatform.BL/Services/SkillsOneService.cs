namespace Beis.LearningPlatform.BL.Services
{
    /// <summary>
    /// A class that defines an implementation of an SkillsOneService.
    /// </summary>
    public class SkillsOneService : ISkillsOneService
    {
        private readonly ISkillsOneDataService _skillsOneDataService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly INotifyIntegrationService _notifyIntegrationService;
        private readonly ISkillsOneService _thisInterface;


        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="notifyIntegrationService">An INotifyIntegrationService that is the Notify integration service to use.</param>
        /// <param name="skillsOneDataService">An ISkillsOneDataService that is the skills one data service to use.</param>
        public SkillsOneService(ILogger<EmailService> logger,
                            IMapper mapper,
                            INotifyIntegrationService notifyIntegrationService,
                            ISkillsOneDataService skillsOneDataService)
        {
            _logger = logger;
            _mapper = mapper;
            _notifyIntegrationService = notifyIntegrationService;
            _skillsOneDataService = skillsOneDataService;

            _thisInterface = this;
        }

        public async Task<IServiceResponse<int>> SaveSkillsOneResponse(Guid requestID, SkillsOneResponseDto skillsOneResponseDto)
        {
            if (skillsOneResponseDto == default)
            {
                throw new ArgumentNullException(nameof(skillsOneResponseDto));
            }

            var returnValue = await _skillsOneDataService.Add(skillsOneResponseDto);
            
            return new ServiceResponse<int>(requestID, true, null, returnValue);
        }
    }
}