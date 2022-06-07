using Beis.LearningPlatform.Data.Repositories.DiagnosticTool;

namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// A class that defines an implementation of an Email data service.
    /// </summary>
    public class EmailDataService : RepositoryDataServiceBase<IDiagnosticToolEmailAnswerRepository>, IEmailDataService
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="dataRepository">An IDataRepository that is the Unit of Work data repository to use.</param>
        /// <param name="repository">An IDiagnosticToolEmailAnswerRepository that is the repository to use.</param>
        public EmailDataService(ILogger<EmailDataService> logger, IMapper mapper, IDataRepository dataRepository, IDiagnosticToolEmailAnswerRepository repository)
            : base(logger, mapper, dataRepository, repository)
        { }

        async Task<int> IEmailDataService.Add(DiagnosticToolEmailAnswerDto diagnosticToolEmailAnswer)
        {
            var entity = _mapper.Map<DiagnosticToolEmailAnswer>(diagnosticToolEmailAnswer);
            entity.Date = DateTime.Now;

            await _repository.AddAsync(entity);
            await SaveAsync();

            return entity.Id;
        }

        async Task<DiagnosticToolEmailAnswerDto[]> IEmailDataService.GetByEmail(string emailAddress)
        {
            var result = (await _repository.Query(x => x.UserEmailAddress == emailAddress)).ToArray();
            var returnValue = _mapper.Map<DiagnosticToolEmailAnswerDto[]>(result);
            return returnValue;
        }

        async Task IEmailDataService.Update(DiagnosticToolEmailAnswerDto diagnosticToolEmailAnswer)
        {
            var entity = _repository.Get(diagnosticToolEmailAnswer.Id);
            _mapper.Map(diagnosticToolEmailAnswer, entity);

            _repository.Update(entity);
            await SaveAsync();
        }
    }
}