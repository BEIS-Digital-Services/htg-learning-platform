namespace Beis.LearningPlatform.DAL
{
    public interface ISkillsTwoDataService
    {
        Task<int> Add(SkillsTwoResponse skillsTwoResponse);
    }
}