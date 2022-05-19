using Beis.LearningPlatform.Data.Entities.Skills;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL
{
    public interface ISkillsThreeDataService
    {
        Task<int> Add(SkillsThreeResponse skillsThreeResponse);
    }
}