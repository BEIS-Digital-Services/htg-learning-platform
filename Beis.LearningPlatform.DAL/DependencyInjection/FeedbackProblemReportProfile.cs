namespace Beis.LearningPlatform.DAL.DependencyInjection
{
    public class FeedbackProblemReportProfile : Profile
    {
        public FeedbackProblemReportProfile()
        {
            CreateMap<FeedbackProblemReport, FeedbackProblemReportDto>();
            CreateMap<FeedbackProblemReportDto, FeedbackProblemReport>();
        }
    }
}