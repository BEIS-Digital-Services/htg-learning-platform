using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library
{
    /// <summary>
    /// A class that defines a DTO for a Diagnostic Tool Email Answer.
    /// </summary>
    /// 
    [ExcludeFromCodeCoverage(Justification = "Basic DTO - only data fields, no functions")]
    public class DiagnosticToolEmailAnswerDto : DtoBase
    {
        /// <summary>
        /// Gets or sets an indication of whether the email address was opted in for marketing.
        /// </summary>
        public bool? IsOptedInForMarketing { get; set; }

        /// <summary>
        /// Gets or sets an indication of whether the privacy policy was accepted.
        /// </summary>
        public bool? IsPrivacyPolicyAccepted { get; set; }

        /// <summary>
        /// Gets or sets an indication of whether the email address has been unsubscribed.
        /// </summary>
        public bool? IsUnsubscribed { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string UserEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the timestamp that the email address was unsubscribed.
        /// </summary>
        public DateTime? UnsubscribeTimestamp { get; set; }

        public string _HowSalesTakePlace { get; set; }
        public string _WhichSector { get; set; }
        public string _SoftwareUsed { get; set; }
        public string _CurrentSupport { get; set; }
        public string _StoreOnPaper { get; set; }
        public string _StoreOnIndividualDrives { get; set; }
        public string _StoreOnSharedDrives { get; set; }
        public string _StoreOnCloud { get; set; }
        public string _SoftwareNeedsKnown { get; set; }
        public string _SoftwareCRM { get; set; }
        public string _SoftwareECommerce { get; set; }
        public string _SoftwareDigitalAccounting { get; set; }
        public string _SoftwareSomethingElse { get; set; }
        public string _SoftwareAdditionalInfo { get; set; }
        public string _SimplifyPaymentsAndListing { get; set; }
        public string _SimplifySellingViaWebsite { get; set; }
        public string _SimplifyCustomerExperiences { get; set; }
        public string _SimplifyTaxAndAccounting { get; set; }
        public string _SimplifySalesInfo { get; set; }
        public string _SimplifyStockLevels { get; set; }
        public string _SimplifyMonitoringSales { get; set; }
        public string _SimplifyCustomersNeeds { get; set; }
        public string _SimplifyCommunication { get; set; }
        public string _SimplifyNone { get; set; }
        public string _SimplifyAdditionalInfo { get; set; }
    }
}