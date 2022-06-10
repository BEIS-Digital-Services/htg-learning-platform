namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// A class that defines an implementation of an Skills Two data service.
    /// </summary>
    public class SkillsTwoDataService : RepositoryDataServiceBase<ISkillsTwoResponseRepository>, ISkillsTwoDataService
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="dataRepository">An IDataRepository that is the Unit of Work data repository to use.</param>
        /// <param name="repository">An ISkillsTwoResponseRepository that is the repository to use.</param>
        public SkillsTwoDataService(ILogger<EmailDataService> logger, IMapper mapper, IDataRepository dataRepository, ISkillsTwoResponseRepository repository)
            : base(logger, mapper, dataRepository, repository)
        { }

        public async Task<int> Add(SkillsTwoResponse skillsTwoResponse)
        {
            skillsTwoResponse.Date = DateTime.Now;

            await _repository.AddAsync(skillsTwoResponse);
            await SaveAsync();

            return skillsTwoResponse.Id;
        }
    }
}