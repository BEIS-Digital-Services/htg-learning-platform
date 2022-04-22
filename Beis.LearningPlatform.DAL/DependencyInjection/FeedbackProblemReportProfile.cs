using AutoMapper;
using Beis.LearningPlatform.Data.Entities.Feedback;
using Beis.LearningPlatform.Library.DTO;

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