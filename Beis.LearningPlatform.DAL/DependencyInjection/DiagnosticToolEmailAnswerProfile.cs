namespace Beis.LearningPlatform.DAL.DependencyInjection
{
    /// <summary>
    /// A class that defines a profile for a Diagnostic Tool Email Answer.
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "To be sorted by ticket LP-1722 which needs to sort code coverage for whole class ")]
    public class DiagnosticToolEmailAnswerProfile : Profile
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public DiagnosticToolEmailAnswerProfile()
        {
            //CreateMap<DiagnosticToolEmailAnswer, DiagnosticToolEmailAnswerDto>()
            //    .ForMember(dest => dest._HowSalesTakePlace, opt => opt.MapFrom(src => src.HowSalesTakePlace));

            CreateMap<DiagnosticToolEmailAnswerDto, DiagnosticToolEmailAnswer>()
                .ForMember(dest => dest.HowSalesTakePlace, opt => opt.MapFrom(src => src._HowSalesTakePlace))
                .ForMember(dest => dest.WhichSector, opt => opt.MapFrom(src => src._WhichSector))
                .ForMember(dest => dest.SoftwareUsed, opt => opt.MapFrom(src => src._SoftwareUsed))
                .ForMember(dest => dest.CurrentSupport, opt => opt.MapFrom(src => src._CurrentSupport))
                .ForMember(dest => dest.StoreOnPaper, opt => opt.MapFrom(src => src._StoreOnPaper))
                .ForMember(dest => dest.StoreOnIndividualDrives, opt => opt.MapFrom(src => src._StoreOnIndividualDrives))
                .ForMember(dest => dest.StoreOnSharedDrives, opt => opt.MapFrom(src => src._StoreOnSharedDrives))
                .ForMember(dest => dest.StoreOnCloud, opt => opt.MapFrom(src => src._StoreOnCloud))
                .ForMember(dest => dest.SoftwareNeedsKnown, opt => opt.MapFrom(src => src._SoftwareNeedsKnown))
                .ForMember(dest => dest.SoftwareCRM, opt => opt.MapFrom(src => src._SoftwareCRM))
                .ForMember(dest => dest.SoftwareECommerce, opt => opt.MapFrom(src => src._SoftwareECommerce))
                .ForMember(dest => dest.SoftwareDigitalAccounting, opt => opt.MapFrom(src => src._SoftwareDigitalAccounting))
                .ForMember(dest => dest.SoftwareSomethingElse, opt => opt.MapFrom(src => src._SoftwareSomethingElse))
                .ForMember(dest => dest.SoftwareAdditionalInfo, opt => opt.MapFrom(src => src._SoftwareAdditionalInfo))
                .ForMember(dest => dest.SimplifyPaymentsAndListing, opt => opt.MapFrom(src => src._SimplifyPaymentsAndListing))
                .ForMember(dest => dest.SimplifyCustomerExperiences, opt => opt.MapFrom(src => src._SimplifyCustomerExperiences))
                .ForMember(dest => dest.SimplifySellingViaWebsite, opt => opt.MapFrom(src => src._SimplifySellingViaWebsite))
                .ForMember(dest => dest.SimplifyTaxAndAccounting, opt => opt.MapFrom(src => src._SimplifyTaxAndAccounting))
                .ForMember(dest => dest.SimplifySalesInfo, opt => opt.MapFrom(src => src._SimplifySalesInfo))
                .ForMember(dest => dest.SimplifyStockLevels, opt => opt.MapFrom(src => src._SimplifyStockLevels))
                .ForMember(dest => dest.SimplifyMonitoringSales, opt => opt.MapFrom(src => src._SimplifyMonitoringSales))
                .ForMember(dest => dest.SimplifyCustomersNeeds, opt => opt.MapFrom(src => src._SimplifyCustomersNeeds))
                .ForMember(dest => dest.SimplifyCommunication, opt => opt.MapFrom(src => src._SimplifyCommunication))
                .ForMember(dest => dest.SimplifyNone, opt => opt.MapFrom(src => src._SimplifyNone))
                .ForMember(dest => dest.SimplifyAdditionalInfo, opt => opt.MapFrom(src => src._SimplifyAdditionalInfo)).ReverseMap();
        }
    }
}