using AutoMapper;
using Beis.LearningPlatform.Data.Entities.Feedback;
using Beis.LearningPlatform.Data.Repositories;
using Beis.LearningPlatform.Data.Repositories.Feedback;
using Beis.LearningPlatform.Library.DTO;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL
{
    /// <summary>
    /// A class that defines an implementation of an Email data service.
    /// </summary>
    public class FeedbackProblemReportDataService : RepositoryDataServiceBase<IFeedbackProblemReportRepository>, IFeedbackProblemReportDataService
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="dataRepository">An IDataRepository that is the Unit of Work data repository to use.</param>
        /// <param name="repository">An IFeedbackPageUsefulRepository that is the repository to use.</param>
        public FeedbackProblemReportDataService(ILogger<EmailDataService> logger, IMapper mapper, IDataRepository dataRepository, IFeedbackProblemReportRepository repository)
            : base(logger, mapper, dataRepository, repository)
        { }

        async Task<int> IFeedbackProblemReportDataService.Add(FeedbackProblemReportDto feedbackProblemReport)
        {
            var entity = _mapper.Map<FeedbackProblemReport>(feedbackProblemReport);

            await _repository.AddAsync(entity);
            await SaveAsync();

            return entity.Id;
        }

        async Task<FeedbackProblemReportDto[]> IFeedbackProblemReportDataService.GetAll()
        {
            var result = (await _repository.GetAllAsync()).ToArray();
            var returnValue = _mapper.Map<FeedbackProblemReportDto[]>(result);
            return returnValue;
        }

        async Task IFeedbackProblemReportDataService.Update(FeedbackProblemReportDto feedbackProblemReport)
        {
            var entity = _repository.Get(feedbackProblemReport.Id);
            _mapper.Map(feedbackProblemReport, entity);

            _repository.Update(entity);
            await SaveAsync();
        }
    }
}