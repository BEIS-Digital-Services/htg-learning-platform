namespace Beis.LearningPlatform.BL.DependencyInjection
{
    /// <summary>
    /// A class that defines a profile for a Diagnostic Tool results email data.
    /// </summary>
    public class DiagnosticToolResultsEmailDataProfile : Profile
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public DiagnosticToolResultsEmailDataProfile()
        {
            CreateMap<DiagnosticToolResultsEmailRelatedArticle, DiagnosticToolResultsEmailRelatedArticleDto>()
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SessionId, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opt => opt.Ignore());
            CreateMap<DiagnosticToolResultsEmailData, DiagnosticToolResultsEmailDataDto>()
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SessionId, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opt => opt.Ignore());

            CreateMap<DiagnosticToolResultsEmailRelatedArticleDto, DiagnosticToolResultsEmailRelatedArticle>();
            CreateMap<DiagnosticToolResultsEmailDataDto, DiagnosticToolResultsEmailData>();
        }
    }
}