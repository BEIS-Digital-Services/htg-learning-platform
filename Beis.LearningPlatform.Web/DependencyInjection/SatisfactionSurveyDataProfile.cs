using System.Text.Encodings.Web;

namespace Beis.LearningPlatform.Web.DependencyInjection
{
    public class SatisfactionSurveyDataProfile : Profile
    {
        public SatisfactionSurveyDataProfile()
        {
            CreateMap<SatisfactionSurveyViewModel, SatisfactionSurveyDto>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(x => HtmlEncoder.Default.Encode(x.Comment)))
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SessionId, opt => opt.Ignore());
        }
    }
}