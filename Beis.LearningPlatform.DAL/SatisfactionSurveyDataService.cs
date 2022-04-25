using AutoMapper;
using Beis.LearningPlatform.Data.Entities.SatisfactionSurvey;
using Beis.LearningPlatform.Data.Repositories;
using Beis.LearningPlatform.Data.Repositories.SatisfactionSurvey;
using Beis.LearningPlatform.Library;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL
{
    public class SatisfactionSurveyDataService : RepositoryDataServiceBase<ISatisfactionSurveyRepository>, ISatisfactionSurveyDataService
    {
        public SatisfactionSurveyDataService(ILogger<SatisfactionSurveyDataService> logger, IMapper mapper, IDataRepository dataRepository, ISatisfactionSurveyRepository repository)
            : base(logger, mapper, dataRepository, repository)
        {
        }

        public async Task<int> Add(SatisfactionSurveyDto satisfactionSurveyDto)
        {
            var entity = _mapper.Map<SatisfactionSurveyEntry>(satisfactionSurveyDto);
            entity.Date = DateTime.Now;

            await _repository.AddAsync(entity);
            await SaveAsync();

            return entity.Id;
        }
    }
}