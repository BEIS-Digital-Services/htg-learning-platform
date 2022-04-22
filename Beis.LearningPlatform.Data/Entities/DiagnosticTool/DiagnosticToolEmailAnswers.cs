using Beis.LearningPlatform.Data.Entities.Base;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Data.Entities.DiagnosticTool
{
    [ExcludeFromCodeCoverage(Justification = "To be sorted by ticket LP-1722 which needs to sort code coverage for whole class ")]
    public class DiagnosticToolEmailAnswer : FeedbackEntity
    {
        public string UserEmailAddress { get; set; }
        public bool? IsPrivacyPolicyAccepted { get; set; }
        public bool? IsOptedInForMarketing { get; set; }

        /// <summary>
        /// Gets or sets an indication of whether the email address has been unsubscribed.
        /// </summary>
        public bool? IsUnsubscribed { get; set; }

        /// <summary>
        /// Gets or sets the timestamp that the email address was unsubscribed.
        /// </summary>
        public DateTime? UnsubscribeTimestamp { get; set; }

        public string HowSalesTakePlace { get; set; }
        public string WhichSector { get; set; }
        public string SoftwareUsed { get; set; }
        public string CurrentSupport { get; set; }
        public string StoreOnPaper { get; set; }
        public string StoreOnIndividualDrives { get; set; }
        public string StoreOnSharedDrives { get; set; }
        public string StoreOnCloud { get; set; }
        public string SoftwareNeedsKnown { get; set; }

        public string SoftwareCRM { get; set; }
        public string SoftwareECommerce { get; set; }
        public string SoftwareDigitalAccounting { get; set; }
        public string SoftwareSomethingElse { get; set; }
        public string SoftwareAdditionalInfo { get; set; }

        public string SimplifyPaymentsAndListing { get; set; }
        public string SimplifySellingViaWebsite { get; set; }
        public string SimplifyCustomerExperiences { get; set; }
        public string SimplifyTaxAndAccounting { get; set; }
        public string SimplifySalesInfo { get; set; }
        public string SimplifyStockLevels { get; set; }
        public string SimplifyMonitoringSales { get; set; }
        public string SimplifyCustomersNeeds { get; set; }
        public string SimplifyCommunication { get; set; }
        public string SimplifyNone { get; set; }
        public string SimplifyAdditionalInfo { get; set; }
    }
}