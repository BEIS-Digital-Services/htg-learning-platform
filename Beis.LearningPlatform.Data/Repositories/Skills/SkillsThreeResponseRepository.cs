namespace Beis.LearningPlatform.Data.Repositories.Skills
{
    /// <summary>
    /// A class that defines an implementation of a Skills Three Response repository.
    /// </summary>
    public class SkillsThreeResponseRepository : FeedbackGenericRepository<SkillsThreeResponse>, ISkillsThreeResponseRepository
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="context">A DataContext that is the data context to use.</param>
        public SkillsThreeResponseRepository(DataContext context)
            : base(context)
        { }
        public SkillsThreeResponse FindByUniqueId(string uniqueId)
        {
            return context.SkillsThreeResponse.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UniqueId == uniqueId);
        }
    }
}