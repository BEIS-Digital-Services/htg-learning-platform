namespace Beis.LearningPlatform.DAL
{
    public class ExpressionOfInterestDataService : RepositoryDataServiceBase<IExpressionOfInterestRepository>, IExpressionOfInterestDataService
    {
        public ExpressionOfInterestDataService(ILogger<EmailDataService> logger, IMapper mapper, IDataRepository dataRepository, IExpressionOfInterestRepository repository)
            : base(logger, mapper, dataRepository, repository)
        { }

        public async Task<int> Add(ExpressionOfInterestDto expressionOfInterestDto)
        {
            var entity = _mapper.Map<ExpressionOfInterest>(expressionOfInterestDto);
            entity.RecordCreatedUtc = DateTime.UtcNow;

            await _repository.AddAsync(entity);
            await SaveAsync();

            return entity.Id;
        }
    }
}