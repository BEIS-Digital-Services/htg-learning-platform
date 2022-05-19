using Beis.LearningPlatform.Data.Entities.Skills;
using Beis.LearningPlatform.Data.Repositories.Base;

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
    }
}