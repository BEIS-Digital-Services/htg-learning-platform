using AutoMapper;
using Beis.LearningPlatform.Data.Entities.Skills;
using Beis.LearningPlatform.Library;

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