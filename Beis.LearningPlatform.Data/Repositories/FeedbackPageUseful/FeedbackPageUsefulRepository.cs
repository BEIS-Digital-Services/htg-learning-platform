using Beis.LearningPlatform.Data.Entities.Feedback;
using Beis.LearningPlatform.Data.Repositories.Base;

namespace Beis.LearningPlatform.Data.Repositories.Feedback
{
    public   class FeedbackPageUsefulRepository : FeedbackGenericRepository<FeedbackPageUseful>, IFeedbackPageUsefulRepository
    {
        public FeedbackPageUsefulRepository(DataContext context) : base(context)
        {
        }
    }
}