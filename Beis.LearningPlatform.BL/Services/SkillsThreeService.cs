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
    /// A class that defines an implementation of an SkillsThreeService.
    /// </summary>
    public class SkillsThreeService : ISkillsThreeService
    {
        private readonly ISkillsThreeDataService _skillsThreeDataService;
        
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="emailDataService">An IEmailDataService that is the email data service to use.</param>
        public SkillsThreeService(ISkillsThreeDataService skillsThreeDataService)
        {
            _skillsThreeDataService = skillsThreeDataService;

        }

        public async Task<IServiceResponse<int>> SaveSkillsThreeResponse(Guid requestID, SkillsThreeResponse skillsThreeResponse)
        {
            bool isSuccessful = false;
            string message = default;
            int returnValue = default;

            if (skillsThreeResponse != default)
            {
                returnValue = await _skillsThreeDataService.Add(skillsThreeResponse);
                isSuccessful = true;
            }
            else
                throw new ArgumentNullException(nameof(skillsThreeResponse));

            return new ServiceResponse<int>(requestID, isSuccessful, message, returnValue);
        }
    }
}