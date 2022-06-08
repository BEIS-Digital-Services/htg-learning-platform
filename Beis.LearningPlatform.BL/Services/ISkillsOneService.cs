namespace Beis.LearningPlatform.BL.Services
{
    public interface ISkillsOneService
    {
        Task<IServiceResponse<int>> SaveSkillsOneResponse(Guid requestID, SkillsOneResponseDto skillsOneResponseDto);
    }
}