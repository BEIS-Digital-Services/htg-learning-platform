namespace Beis.LearningPlatform.Web.DependencyInjection
{
    /// <summary>
    /// A class that defines a profile for an Email Answer.
    /// </summary>
    public class DiagnosticToolEmailAnswerProfile : Profile
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public DiagnosticToolEmailAnswerProfile()
        {
            CreateMap<EmailAnswer, DiagnosticToolEmailAnswerDto>()
                .ForMember(dest => dest.IsUnsubscribed, opt => opt.Ignore())
                .ForMember(dest => dest.UnsubscribeTimestamp, opt => opt.Ignore())
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SessionId, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opt => opt.Ignore())
                .ForMember(dest => dest._HowSalesTakePlace, opt => opt.Ignore())
                .ForMember(dest => dest._WhichSector, opt => opt.Ignore())
                .ForMember(dest => dest._SoftwareUsed, opt => opt.Ignore())
                .ForMember(dest => dest._CurrentSupport, opt => opt.Ignore())
                .ForMember(dest => dest._StoreOnPaper, opt => opt.Ignore())
                .ForMember(dest => dest._StoreOnIndividualDrives, opt => opt.Ignore())
                .ForMember(dest => dest._StoreOnSharedDrives, opt => opt.Ignore())
                .ForMember(dest => dest._StoreOnCloud, opt => opt.Ignore())
                .ForMember(dest => dest._SoftwareNeedsKnown, opt => opt.Ignore())
                .ForMember(dest => dest._SoftwareCRM, opt => opt.Ignore())
                .ForMember(dest => dest._SoftwareECommerce, opt => opt.Ignore())
                .ForMember(dest => dest._SoftwareDigitalAccounting, opt => opt.Ignore())
                .ForMember(dest => dest._SoftwareSomethingElse, opt => opt.Ignore())
                .ForMember(dest => dest._SoftwareAdditionalInfo, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifyPaymentsAndListing, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifySellingViaWebsite, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifyCustomerExperiences, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifyTaxAndAccounting, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifySalesInfo, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifyStockLevels, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifyMonitoringSales, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifyCustomersNeeds, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifyCommunication, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifyNone, opt => opt.Ignore())
                .ForMember(dest => dest._SimplifyAdditionalInfo, opt => opt.Ignore());

            CreateMap<DiagnosticToolEmailAnswerDto, EmailAnswer>()
                .ForMember(dest => dest.EmailErrorText, opt => opt.Ignore())
                .ForMember(dest => dest.HasAcceptedPrivacyPolicyText, opt => opt.Ignore())
                .ForMember(dest => dest.HasConsentedToMarketingText, opt => opt.Ignore())
                .ForMember(dest => dest.ErrorHeading, opt => opt.Ignore())
                .ForMember(dest => dest.HasEmailError, opt => opt.Ignore());
        }
    }
}