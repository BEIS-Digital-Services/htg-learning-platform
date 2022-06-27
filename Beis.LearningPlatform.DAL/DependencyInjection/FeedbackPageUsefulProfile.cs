namespace Beis.LearningPlatform.DAL.DependencyInjection
{
    public class FeedbackPageUsefulProfile : Profile
    {
        public FeedbackPageUsefulProfile()
        {
            CreateMap<FeedbackPageUseful, FeedbackPageUsefulDto>();
            CreateMap<FeedbackPageUsefulDto, FeedbackPageUseful>();
        }
    }
}