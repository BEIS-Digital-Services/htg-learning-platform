namespace Beis.LearningPlatform.DAL
{
    public interface ISkillsThreeDataService
    {
        Task<int> Add(SkillsThreeResponse skillsThreeResponse);
        public SkillsThreeResponse FindByUniqueId(string uniqueId);
    }
}