namespace Beis.LearningPlatform.Data.Repositories.ExpressionOfInterest
{
    public class ExpressionOfInterestRepository : GenericRepository<Entities.ExpressionOfInterest>, IExpressionOfInterestRepository
    {
        public ExpressionOfInterestRepository(DataContext context) : base(context)
        {
        }
    }
}