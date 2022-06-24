namespace Beis.LearningPlatform.BL.DependencyInjection
{
    public class FeedbackPageUsefulProfile : Profile
    {
        public FeedbackPageUsefulProfile()
        {
            CreateMap<CMSFeedbackPageUsefulBM, FeedbackPageUsefulDto>()
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<FeedbackPageUsefulDto, CMSFeedbackPageUsefulBM>();
        }
    }
}