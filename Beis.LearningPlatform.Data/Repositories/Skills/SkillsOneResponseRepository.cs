using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Data.Repositories.Skills
{
    /// <summary>
    /// A class that defines an implementation of a Skills One Response repository.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SkillsOneResponseRepository : FeedbackGenericRepository<SkillsOneResponse>, ISkillsOneResponseRepository
    {
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="context">A DataContext that is the data context to use.</param>
        public SkillsOneResponseRepository(DataContext context)
            : base(context)
        { }
    }
}