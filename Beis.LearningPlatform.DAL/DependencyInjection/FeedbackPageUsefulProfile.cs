using AutoMapper;
using Beis.LearningPlatform.Data.Entities.Feedback;
using Beis.LearningPlatform.Library.DTO;

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