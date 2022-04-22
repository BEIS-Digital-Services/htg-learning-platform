namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// A class that defines an email answer for the Diagnostic Tool.
    /// </summary>
    public class EmailAnswer
    {
        /// <summary>
        /// Gets or sets the error text to be shown when there is an email error.
        /// </summary>
        public string EmailErrorText { get; set; }

        /// <summary>
        /// Gets or sets a textual indication of whether the user has accepted the privacy policy.
        /// </summary>
        public string HasAcceptedPrivacyPolicyText
        {
            get => IsPrivacyPolicyAccepted ? "on" : null;
            set => IsPrivacyPolicyAccepted = value == "on";
        }

        /// <summary>
        /// Gets or sets a textual indication of whether the user has consented to receive marketing correspondence.
        /// </summary>
        public string HasConsentedToMarketingText
        {
            get => IsOptedInForMarketing ? "on" : null;
            set => IsOptedInForMarketing = value == "on";
        }

        /// <summary>
        /// Gets an indication of whether an email address has been entered.
        /// </summary>
        public bool HasEmailAddress => !string.IsNullOrWhiteSpace(UserEmailAddress);

        public string ErrorHeading { get; set; }

        /// <summary>
        /// Gets or sets an indication of whether there is an email error.
        /// </summary>
        public bool HasEmailError { get; set; }

        /// <summary>
        /// Gets or sets an indication of whether the email address was opted in for marketing.
        /// </summary>
        public bool IsOptedInForMarketing { get; set; }

        /// <summary>
        /// Gets or sets an indication of whether the privacy policy was accepted.
        /// </summary>
        public bool IsPrivacyPolicyAccepted { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string UserEmailAddress { get; set; }
    }
}