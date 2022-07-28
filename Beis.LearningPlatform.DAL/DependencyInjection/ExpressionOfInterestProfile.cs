using System.Text.Encodings.Web;

namespace Beis.LearningPlatform.DAL.DependencyInjection
{
    public class ExpressionOfInterestProfile : Profile
    {
        public ExpressionOfInterestProfile()
        {
            CreateMap<ExpressionOfInterestDto, ExpressionOfInterest>()
                .ForMember(dest => dest.PageName, opt => opt.MapFrom(x => HtmlEncoder.Default.Encode(x.PageName)))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => HtmlEncoder.Default.Encode(x.UserName)))
                .ForMember(dest => dest.UserBusinessName, opt => opt.MapFrom(x => HtmlEncoder.Default.Encode(x.UserBusinessName)))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(x => HtmlEncoder.Default.Encode(x.UserEmail)))
                .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(x => HtmlEncoder.Default.Encode(x.UserPhone)));
        }
    }
}