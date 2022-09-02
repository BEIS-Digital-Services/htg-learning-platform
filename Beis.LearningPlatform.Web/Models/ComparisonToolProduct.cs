namespace Beis.LearningPlatform.Web.ComparisonTool.Models
{
    public class ComparisonToolProduct : product
    {
        public ComparisonToolProduct()
        {

        }

        /// <summary>
        /// Gets or Sets the list of product_prices from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_price> productPrices { get; set; }

        #region Child Relationship Repository calls

        /// <summary>
        /// Gets or Sets the list of settings_product_capabilities from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<settings_product_capability> settingsProductCapabilities { get; set; }

        /// <summary>
        /// Gets or Sets the list of product_capability from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_capability> productCapabilities { get; set; }


        /// <summary>
        /// Gets or Sets the list of training (fiterType = 2) settings_product_filters from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<settings_product_filter> settingsProductTrainingFilters { get; set; }

        /// <summary>
        /// Gets or Sets the list of support (fiterType = 1) settings_product_filters from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<settings_product_filter> settingsProductSupportFilters { get; set; }

        /// <summary>
        /// Gets or Sets the list of platform compatibility (fiterType = 3) settings_product_filters from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<settings_product_filter> settingsProductPltatformCompatibilityFilters { get; set; }

        /// <summary>
        /// Gets or Sets the list of product_filter from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_filter> productFilters { get; set; }

        /// <summary>
        /// Gets or Sets the list of training (fiterType = 2) product_filter from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_filter> productTrainingFilters { get; set; }

        /// <summary>
        /// Gets or Sets the list of support (fiterType = 1) product_filter from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_filter> productSupportFilters { get; set; }

        /// <summary>
        /// Gets or Sets the list of platform compatibility (fiterType = 3) product_filter from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_filter> productPltatformCompatibilityFilters { get; set; }

        /// <summary>
        /// Gets or Sets the list of product_price_base_metric_price from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_price_base_metric_price> productPriceBaseMetricPrice { get; set; }

        /// <summary>
        /// Gets or Sets the list of product_price_base_description from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_price_base_description> productPriceBaseMetricDescription { get; set; }

        /// <summary>
        /// Gets or Sets the list of user_discount from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<user_discount> productUserDiscount { get; set; }

        /// <summary>
        /// Gets or Sets the list of product_price_secondary_description from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_price_secondary_description> productPriceSecondaryDescriptions { get; set; }

        /// <summary>
        /// Gets or Sets the list of product_price_secondary_metric from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<product_price_secondary_metric> productPriceSecondaryMetrics { get; set; }

        /// <summary>
        /// Gets or Sets the list of additional_cost_desc from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<additional_cost_desc> productPriceAddCostDescriptions { get; set; }

        /// <summary>
        /// Gets or Sets the list of additional_cost from the Help_To_Grow database (Vendor Team)
        /// </summary>
        public IList<ComparisonToolAdditionalCost> productPriceAddCosts { get; set; }
        public IList<ComparisonToolAdditionalCost> productPriceTransactionFees { get; set; }
        public IList<ComparisonToolAdditionalCost> productPriceThirdPartyFees { get; set; }

        public string GetTransactionFeeCost(string description)
        {
            return this.productPriceTransactionFees?.FirstOrDefault(x => x.CostDescription == description)?.CostAndFrequency;
        }

        public string GetThirdPartyFeeCost(string description)
        {
            return this.productPriceThirdPartyFees?.FirstOrDefault(x => x.CostDescription == description)?.CostAndFrequency;
        }

        #endregion


        #region View Display Labels created as readonly properties as they are condittionally calculated
        /// <summary>
        /// Gets a conditionally calculated label for view display of the Contract Term
        /// </summary>
        public string contractTerm { 
            get
            {
                return (productPrices.Count > 0 ? $"{productPrices[0].commitment_no} {productPrices[0].commitment_unit}" : "None");
            }
        }

        /// <summary>
        /// Gets a conditionally calculated label for view display of the Payment Term
        /// </summary>
        public string paymentTerm
        {
            // ATTENTION: The ComparisonToolFieldValidator is responsible for displaying visual cues on the Web App to alert us to potentially 
            // INCORRECT DATA OR DATA MAPPING. This is meant to be a Temporary feature. Please check with Pritesh Patel when to depricate the feature
            get
            {
                return (productPriceBaseMetricPrice?.Count > 0 ? ComparisonToolFieldValidator.ParseStringField(productPriceBaseMetricDescription.Where(item => item.product_price_base_description_id == productPriceBaseMetricPrice.FirstOrDefault()?.product_price_base_description_id).FirstOrDefault()?.product_price_basedescription) : "No productPriceBaseMetricPrice Record");
            }
        }

        /// <summary>
        /// Gets a conditionally calculated label for view display of the Free Trial Term
        /// </summary>
        public string freeTrial
        {
            // ATTENTION: The ComparisonToolFieldValidator is responsible for displaying visual cues on the Web App to alert us to potentially 
            // INCORRECT DATA OR DATA MAPPING. This is meant to be a Temporary feature. Please check with Pritesh Patel when to depricate the feature
            get
            {
                return (productPrices.Count > 0 ? ComparisonToolFieldValidator.ParseStringField($"{productPrices[0].free_trial_flag.ToString().Replace("False", "No").Replace("True", "Yes")}{(productPrices[0].free_trial_flag ? $", {productPrices[0].free_trial_term_no} {productPrices[0].free_trial_term_unit}" : string.Empty)}") : "No ProductPrice Record");
            }
        }

        /// <summary>
        /// Gets a conditionally calculated label for view display of the Price Payment Term
        /// </summary>
        public string pricePaymentTerm
        {
            get
            {
                return ((paymentTerm?.Split(","))[0]);
            }
        }

        /// <summary>
        /// Gets a conditionally calculated label for view display of the Price
        /// </summary>
        public string priceDisplayed
        {
            get
            {
                return (productPriceBaseMetricPrice?.Count > 0 ? $"{productPriceBaseMetricPrice[0].product_price_amount.ToString("C2", new CultureInfo("en-GB"))}, {pricePaymentTerm}" : "No productPriceBaseMetricPrice Record");
            }
        }

        /// <summary>
        /// Gets a conditionally calculated label for view display of the Minimum number of Licences
        /// </summary>
        public string minLicences
        {
            // ATTENTION: The ComparisonToolFieldValidator is responsible for displaying visual cues on the Web App to alert us to potentially 
            // INCORRECT DATA OR DATA MAPPING. This is meant to be a Temporary feature. Please check with Pritesh Patel when to depricate the feature
            get
            {
                return (productPrices.Count > 0 ? ComparisonToolFieldValidator.ParseStringField(productPrices[0].min_no_users > 0 ? productPrices[0].min_no_users.ToString() : "None") : "No ProductPrice Record");
            }
        }

        /// <summary>
        /// Gets a conditionally calculated label for view display of the Contract Discount
        /// </summary>
        public string contractDiscount
        {
            get
            {
                if (productPrices == null || !productPrices.Any()) 
                {
                    return "No ProductPrice Record"; //TP-TODO: As part of refactor just hide these records - don't show error messages on the UI.
                }

                if (!productPrices[0].discount_flag)
                {
                    return "None";
                }

                if (productPrices[0].discount_percentage == 0 && productPrices[0].discount_price == 0)
                { 
                    return "None";
                }

                var discountTerm = ComparisonToolFieldValidator.ParseStringField(productPrices[0].discount_term_no.ToString());
                var discountUnit = ComparisonToolFieldValidator.ParseStringField(productPrices[0].discount_term_unit);

                if (productPrices[0].discount_percentage > 0)
                {
                    return $"{discountTerm} {discountUnit} at {productPrices[0].discount_percentage}% off";
                }
                else
                { 
                    return $"{discountTerm} {discountUnit} at £{productPrices[0].discount_price:f2}";                
                }
            }
        }

        public bool HasContractDurationDiscountUnitAndPercentage
        {
            get
            {
                return !string.IsNullOrEmpty(contractDurationDiscount);
            }
        }

        /// <summary>
        /// Gets a conditionally calculated label for view display of the Contract Duration Discount
        /// </summary>
        public string contractDurationDiscount
        {
            // ATTENTION: The ComparisonToolFieldValidator is responsible for displaying visual cues on the Web App to alert us to potentially 
            // INCORRECT DATA OR DATA MAPPING. This is meant to be a Temporary feature. Please check with Pritesh Patel when to depricate the feature
            get
            {
                if (productPrices?.Any() != true)
                {
                    return null;
                }

                if (string.IsNullOrEmpty(productPrices[0].contract_duration_discount_unit) || productPrices[0].contract_duration_discount_percentage == default)
                { 
                    return null;
                }

                return $"{ComparisonToolFieldValidator.ParseStringField(productPrices[0].contract_duration_discount_unit)} at {ComparisonToolFieldValidator.ParseStringField(productPrices[0].contract_duration_discount_percentage.ToString())}% off";
            }
        }

        private int? _productPriceNumberOfUsers;
        public int? ProductPriceNumberOfUsers
        {
            get
            {
                if (_productPriceNumberOfUsers.HasValue) 
                {
                    return _productPriceNumberOfUsers.Value == default ? null : _productPriceNumberOfUsers.Value;
                }

                if (!this.HasProductPriceBaseMetricPrices(out product_price_base_metric_price firstBaseMetricPrice))
                {
                    _productPriceNumberOfUsers = 0; // Cache this lookup
                    return null;
                }

                if (firstBaseMetricPrice.product_price_no_users == default)
                {
                    _productPriceNumberOfUsers = 0; // Cache this lookup
                    return null;
                }

                _productPriceNumberOfUsers = firstBaseMetricPrice.product_price_no_users;
                return _productPriceNumberOfUsers.Value;
            }
        }

        private bool HasProductPriceBaseMetricPrices(out product_price_base_metric_price firstBaseMetricPrice)
        {            
            firstBaseMetricPrice = productPrices?.FirstOrDefault()?.product_price_base_metric_prices?.FirstOrDefault();
            return firstBaseMetricPrice != null;
        }

        public bool HasAdditionalDiscounts()
        {
            var productPrice = productPrices.FirstOrDefault();
            if (productPrice == null)
            {
                return false;
            }

            return productPrice.discount_flag ||
                    productPrice.contract_duration_discount_flag ||
                    (productPrice.user_discount_flag && productUserDiscount?.Count > 0);
        }

        #endregion
    }
}