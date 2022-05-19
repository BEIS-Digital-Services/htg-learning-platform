using Beis.LearningPlatform.BL.Domain;
using Beis.LearningPlatform.Data.Entities.Skills;
using System;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.Services
{
    public interface ISkillsThreeService
    {
        Task<IServiceResponse<int>> SaveSkillsThreeResponse(Guid requestID, SkillsThreeResponse skillsThreeResponse);
    }
}