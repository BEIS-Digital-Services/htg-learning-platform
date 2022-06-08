namespace Beis.LearningPlatform.DAL.DependencyInjection
{
    public class SkillsOneProfile : Profile
    {
        public SkillsOneProfile()
        {
            CreateMap<SkillsOneResponseDto, SkillsOneResponse>().ReverseMap();
        }
    }
}