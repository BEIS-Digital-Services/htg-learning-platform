namespace Beis.LearningPlatform.Data.Repositories.Feedback
{
    public   class FeedbackPageUsefulRepository : FeedbackGenericRepository<FeedbackPageUseful>, IFeedbackPageUsefulRepository
    {
        public FeedbackPageUsefulRepository(DataContext context) : base(context)
        {
        }
    }
}