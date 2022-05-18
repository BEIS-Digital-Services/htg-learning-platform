using AutoMapper;
using Beis.LearningPlatform.Data.Entities.Skills;
using Beis.LearningPlatform.Data.Repositories;
using Beis.LearningPlatform.Data.Repositories.Skills;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// A class that defines an implementation of an Skills Three data service.
    /// </summary>
    public class SkillsThreeDataService : RepositoryDataServiceBase<ISkillsThreeResponseRepository>, ISkillsThreeDataService
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="dataRepository">An IDataRepository that is the Unit of Work data repository to use.</param>
        /// <param name="repository">An ISkillsThreeResponseRepository that is the repository to use.</param>
        public SkillsThreeDataService(ILogger<EmailDataService> logger, IMapper mapper, IDataRepository dataRepository, ISkillsThreeResponseRepository repository)
            : base(logger, mapper, dataRepository, repository)
        { }

        public async Task<int> Add(SkillsThreeResponse skillsThreeResponse)
        {
            skillsThreeResponse.Date = DateTime.Now;

            await _repository.AddAsync(skillsThreeResponse);
            await SaveAsync();

            return skillsThreeResponse.Id;
        }
    }
}