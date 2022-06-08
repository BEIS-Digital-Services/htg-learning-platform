namespace Beis.LearningPlatform.DAL
{
    public interface ISkillsOneDataService
    {
        Task<int> Add(SkillsOneResponseDto skillsOneResponseDto);
    }
}