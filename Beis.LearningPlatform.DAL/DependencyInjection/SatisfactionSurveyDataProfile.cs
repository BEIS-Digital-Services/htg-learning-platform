using AutoMapper;
using Beis.LearningPlatform.Data.Entities.SatisfactionSurvey;
using Beis.LearningPlatform.Library;

namespace Beis.LearningPlatform.DAL.DependencyInjection
{
    public class SatisfactionSurveyDataProfile : Profile
    {
        public SatisfactionSurveyDataProfile()
        {
            CreateMap<SatisfactionSurveyDto, SatisfactionSurveyEntry>();
        }
    }
}