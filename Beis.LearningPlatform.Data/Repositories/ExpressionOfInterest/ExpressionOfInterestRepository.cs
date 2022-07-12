namespace Beis.LearningPlatform.Data.Repositories.ExpressionOfInterest
{
    public class ExpressionOfInterestRepository : GenericRepository<ExpressionOfInterestEntity>, IExpressionOfInterestRepository
    {
        public ExpressionOfInterestRepository(DataContext context) : base(context)
        {
        }
    }
}