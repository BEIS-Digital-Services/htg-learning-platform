using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

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
                        new () { id = 2, name = "crm", displayName = "CUSTOMER RELATIONSHIP MANAGEMENT (CRM) SOFTWARE" },
                        new () { id = 1, name = "accounting", displayName = "DIGITAL ACCOUNTING SOFTWARE" }
                    };

                    // AS Per LP-869: the Ecom products and the ecom tag must only be displayed in the DEV environment AND not be displayed in UAT or Prod environment
                    if (_ctDisplayOption.ShowECommerce ?? false)
                    {
                        _productCategories.Add(new CMSSearchTag() { id = 3, name = "ecommerce", displayName = "ECOMMERCE" });
                    }
                }

                return _productCategories;
            }
        }

        public bool? ShowAllProductStatuses => _ctDisplayOption.ShowAllProductStatuses;
    }
}