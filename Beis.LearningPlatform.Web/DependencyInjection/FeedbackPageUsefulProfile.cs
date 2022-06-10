namespace Beis.LearningPlatform.Web.DependencyInjection
{
    public class FeedbackPageUsefulProfile : Profile
    {
        public FeedbackPageUsefulProfile()
        {
            CreateMap<CMSFeedbackPageUseful, FeedbackPageUsefulDto>()
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<FeedbackPageUsefulDto, CMSFeedbackPageUseful>();
            CreateMap<CMSFeedbackPageUseful, CMSFeedbackPageUsefulBM>();
            CreateMap<CMSFeedbackPageUsefulBM, CMSFeedbackPageUseful>();
        }
    }
}