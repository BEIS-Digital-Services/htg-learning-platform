namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// A class that defines an implementation of an Skills One data service.
    /// </summary>
    public class SkillsOneDataService : RepositoryDataServiceBase<ISkillsOneResponseRepository>, ISkillsOneDataService
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="dataRepository">An IDataRepository that is the Unit of Work data repository to use.</param>
        /// <param name="repository">An ISkillsOneResponseRepository that is the repository to use.</param>
        public SkillsOneDataService(ILogger<EmailDataService> logger, IMapper mapper, IDataRepository dataRepository, ISkillsOneResponseRepository repository)
            : base(logger, mapper, dataRepository, repository)
        { }

        public async Task<int> Add(SkillsOneResponseDto skillsOneResponseDto)
        {
            var entity = _mapper.Map<SkillsOneResponse>(skillsOneResponseDto);
            entity.Date = DateTime.UtcNow;

            await _repository.AddAsync(entity);
            await SaveAsync();

            return entity.Id;
        }
    }
}