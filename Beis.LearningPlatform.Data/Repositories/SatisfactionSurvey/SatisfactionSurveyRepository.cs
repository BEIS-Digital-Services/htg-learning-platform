using Beis.LearningPlatform.Data.Entities.SatisfactionSurvey;
using Beis.LearningPlatform.Data.Repositories.Base;

namespace Beis.LearningPlatform.Data.Repositories.SatisfactionSurvey
{
    public class SatisfactionSurveyRepository : FeedbackGenericRepository<SatisfactionSurveyEntry>, ISatisfactionSurveyRepository
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="context">A DataContext that is the data context to use.</param>
        public SatisfactionSurveyRepository(DataContext context)
            : base(context)
        { }
    }
}