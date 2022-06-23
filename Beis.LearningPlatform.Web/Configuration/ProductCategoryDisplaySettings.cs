namespace Beis.LearningPlatform.Web.Configuration
{
    public class ProductCategoryDisplaySettings : IProductCategoryDisplaySettings
    {
        private List<CMSSearchTag> _productCategories;
        private readonly ComparisonToolDisplayOption _ctDisplayOption;

        public ProductCategoryDisplaySettings(IOptions<ComparisonToolDisplayOption> ctDisplayOptions)
        {
            _ctDisplayOption = ctDisplayOptions.Value;
        }

        public IList<CMSSearchTag> DisplaySettings
        {
            get
            {
                if (_productCategories == null)
                {
                    _productCategories = new List<CMSSearchTag>
                    {
                        new () { id = 2, name = "crm", displayName = "CUSTOMER RELATIONSHIP MANAGEMENT (CRM) SOFTWARE", friendlyDisplayName = "Customer Relationship Management" },
                        new () { id = 1, name = "accounting", displayName = "DIGITAL ACCOUNTING SOFTWARE", friendlyDisplayName = "Digital Accounting" },
                        new () { id = 4, name = "cyberSecurity", displayName = "CYBER SECURITY", friendlyDisplayName = "Cyber Security" }
                    };

                    // AS Per LP-869: the Ecom products and the ecom tag must only be displayed in the DEV environment AND not be displayed in UAT or Prod environment
                    if (_ctDisplayOption.ShowECommerce ?? false)
                    {
                        _productCategories.Add(new CMSSearchTag() { id = 3, name = "ecommerce", displayName = "ECOMMERCE", friendlyDisplayName= "eCommerce" });
                    }
                }

                return _productCategories;
            }
        }

        public bool? ShowAllProductStatuses => _ctDisplayOption.ShowAllProductStatuses;
    }
}