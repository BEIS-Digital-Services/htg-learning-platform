namespace Beis.LearningPlatform.DAL.DependencyInjection
{
    public class ExpressionOfInterestProfile : Profile
    {
        public ExpressionOfInterestProfile()
        {
            CreateMap<ExpressionOfInterestDto, ExpressionOfInterest>();
        }
    }
}