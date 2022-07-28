using Beis.LearningPlatform.DAL.Mappers;

namespace Beis.LearningPlatform.DAL
{
    public class ExpressionOfInterestDataService : RepositoryDataServiceBase<IExpressionOfInterestRepository>, IExpressionOfInterestDataService
    {
        public ExpressionOfInterestDataService(ILogger<EmailDataService> logger, IMapper mapper, IDataRepository dataRepository, IExpressionOfInterestRepository repository)
            : base(logger, mapper, dataRepository, repository)
        { }

        public async Task<int> Add(ExpressionOfInterestDto expressionOfInterestDto)
        {
            var entity = ExpressionOfInterestMapper.GetExpressionOfInterest(expressionOfInterestDto);
            entity.RecordCreatedUtc = DateTime.UtcNow;

            await _repository.AddAsync(entity);
            await SaveAsync();

            return entity.Id;
        }
    }
}