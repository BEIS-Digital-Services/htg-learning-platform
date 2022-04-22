using Beis.LearningPlatform.Data.Entities.Skills;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL
{
    public interface ISkillsTwoDataService
    {
        Task<int> Add(SkillsTwoResponse skillsTwoResponse);
    }
}