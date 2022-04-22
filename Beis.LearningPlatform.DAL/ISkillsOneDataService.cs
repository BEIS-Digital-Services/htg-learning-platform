using Beis.LearningPlatform.Library;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.DAL
{
    public interface ISkillsOneDataService
    {
        Task<int> Add(SkillsOneResponseDto skillsOneResponseDto);
    }
}