namespace Beis.LearningPlatform.BL.Services
{
    public interface ISkillsThreeService
    {
        Task<IServiceResponse<int>> SaveSkillsThreeResponse(Guid requestID, SkillsThreeResponse skillsThreeResponse);
        public SkillsThreeResponse FindByUniqueId(string uniqueId);
    }
}