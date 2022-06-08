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