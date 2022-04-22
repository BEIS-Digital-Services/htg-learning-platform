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
    public class FeedbackUsefulDataService : RepositoryDataServiceBase<IFeedbackPageUsefulRepository>, IFeedbackUsefulDataService
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="dataRepository">An IDataRepository that is the Unit of Work data repository to use.</param>
        /// <param name="repository">An IFeedbackPageUsefulRepository that is the repository to use.</param>
        public FeedbackUsefulDataService(ILogger<EmailDataService> logger, IMapper mapper, IDataRepository dataRepository, IFeedbackPageUsefulRepository repository)
            : base(logger, mapper, dataRepository, repository)
        { }

        async Task<int> IFeedbackUsefulDataService.Add(FeedbackPageUsefulDto feedbackUsefulAnswer)
        {
            var entity = _mapper.Map<FeedbackPageUseful>(feedbackUsefulAnswer);

            await _repository.AddAsync(entity);
            await SaveAsync();

            return entity.Id;
        }

        async Task<FeedbackPageUsefulDto[]> IFeedbackUsefulDataService.GetAll()
        {
            var result = (await _repository.GetAllAsync()).ToArray();
            var returnValue = _mapper.Map<FeedbackPageUsefulDto[]>(result);
            return returnValue;
        }

        async Task IFeedbackUsefulDataService.Update(FeedbackPageUsefulDto feedbackUsefulAnswer)
        {
            var entity = _repository.Get(feedbackUsefulAnswer.Id);
            _mapper.Map(feedbackUsefulAnswer, entity);

            _repository.Update(entity);
            await SaveAsync();
        }
    }
}