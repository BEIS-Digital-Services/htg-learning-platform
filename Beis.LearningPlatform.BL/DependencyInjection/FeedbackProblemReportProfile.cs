using System.Text.Encodings.Web;

namespace Beis.LearningPlatform.BL.DependencyInjection
{
    public class FeedbackProblemReportProfile : Profile
    {
        public FeedbackProblemReportProfile()
        {
            CreateMap<CMSFeedbackProblemBM, FeedbackProblemReportDto>()
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.WhatIWasDoing, opt => opt.MapFrom(x => HtmlEncoder.Default.Encode(x.whatIWasDoing)))
                .ForMember(dest => dest.WhatWentWrong, opt => opt.MapFrom(x => HtmlEncoder.Default.Encode(x.whatWentWrong)));
            CreateMap<FeedbackProblemReportDto, CMSFeedbackProblemBM>();
        }
    }
}