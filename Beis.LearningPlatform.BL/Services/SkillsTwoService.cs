using Beis.LearningPlatform.BL.Domain;
using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.DAL;
using Beis.LearningPlatform.Data.Entities.Skills;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
            bool isSuccessful = false;
            string message = default;
            int returnValue = default;

            if (skillsTwoResponse != default)
            {
                returnValue = await _skillsTwoDataService.Add(skillsTwoResponse);
                isSuccessful = true;
            }
            else
                throw new ArgumentNullException(nameof(skillsTwoResponse));

            return new ServiceResponse<int>(requestID, isSuccessful, message, returnValue);
        }
    }
}