using Beis.LearningPlatform.BL.Domain;
using Beis.LearningPlatform.Library;
using System;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.Services
{
    public interface ISkillsOneService
    {
        Task<IServiceResponse<int>> SaveSkillsOneResponse(Guid requestID, SkillsOneResponseDto skillsOneResponseDto);
    }
}