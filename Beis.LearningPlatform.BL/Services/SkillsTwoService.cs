namespace Beis.LearningPlatform.BL.Services
{
    /// <summary>
    /// A class that defines an implementation of an SkillsTwoService.
    /// </summary>
    public class SkillsTwoService : ISkillsTwoService
    {
        private readonly ISkillsTwoDataService _skillsTwoDataService;
        private readonly ILogger _logger;
        private readonly INotifyIntegrationService _notifyIntegrationService;
        private readonly ISkillsTwoService _thisInterface;


        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="notifyIntegrationService">An INotifyIntegrationService that is the Notify integration service to use.</param>
        /// <param name="emailDataService">An IEmailDataService that is the email data service to use.</param>
        public SkillsTwoService(ILogger<EmailService> logger,
                           
                            INotifyIntegrationService notifyIntegrationService,
                            ISkillsTwoDataService skillsTwoDataService)
        {
            _logger = logger;
            _notifyIntegrationService = notifyIntegrationService;
            _skillsTwoDataService = skillsTwoDataService;

            _thisInterface = this;
        }

        public async Task<IServiceResponse<int>> SaveSkillsTwoResponse(Guid requestID, SkillsTwoResponse skillsTwoResponse)
        {
            if (skillsTwoResponse == default)
            {
                throw new ArgumentNullException(nameof(skillsTwoResponse));
                
            }
            
            int returnValue = await _skillsTwoDataService.Add(skillsTwoResponse);

            return new ServiceResponse<int>(requestID, true, null, returnValue);
        }
    }
}