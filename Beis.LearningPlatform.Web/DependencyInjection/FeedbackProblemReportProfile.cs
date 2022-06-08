namespace Beis.LearningPlatform.Web.DependencyInjection
{
    public class FeedbackProblemReportProfile : Profile
    {
        public FeedbackProblemReportProfile()
        {
            CreateMap<CMSFeedbackProblem, FeedbackProblemReportDto>()
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<FeedbackProblemReportDto, CMSFeedbackProblem>();
            CreateMap<CMSFeedbackProblem, CMSFeedbackProblemBM>();
            CreateMap<CMSFeedbackProblemBM, CMSFeedbackProblem>();
        }
    }
}