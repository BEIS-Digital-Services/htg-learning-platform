﻿namespace Beis.LearningPlatform.BL.Services
{
    public interface ISkillsTwoService
    {
        Task<IServiceResponse<int>> SaveSkillsTwoResponse(Guid requestID, SkillsTwoResponse skillsTwoResponse);
    }
}