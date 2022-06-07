using Beis.LearningPlatform.Web.ComparisonTool.Models;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Beis.LearningPlatform.Web.Models
{
    /// <summary>
    /// A wrapper class for list of ComparisonToolProduct objects, CMS objects and User Journey Data to a View Model
    /// that can be consumed by the Comparison Tool views
    /// </summary>
    public class ComparisonToolPageViewModel : IPageViewModel
    {
        /// <summary>
        /// Gets or sets the id of the view model. The value is obtained from the CMS id.
        /// </summary>
        public int id { get; set; }


        /// <summary>
        /// Gets or sets the creation date time. The value is obtained from CMS.
        /// </summary>
        public DateTime created_at { get; set; }

        /// <summary>
        /// Gets or sets the list of CMSPageFooter objects from the CMS system (strapi)
        /// </summary>
        public IList<CMSPageFooter> footers { get; set; }


        /// <summary>
        /// Gets or sets the list of CMSPageNavigation objects from the CMS system (strapi)
        /// </summary>
        public IList<CMSPageNavigation> navigations { get; set; }

        public PartialNavigationModel NavigationModel => this.GetPartialNavigationModel();

        /// <summary>
        /// Gets or sets the published date time. The value is obtained from CMS.
        /// </summary>
        public DateTime published_at { get; set; }

        /// <summary>
        /// Gets or sets the updated date time. The value is obtained from CMS.
        /// </summary>
        public DateTime updated_at { get; set; }

        /// <summary>
        /// Gets or sets the list of CMSPageSideNavigation objects from the CMS system (strapi)
        /// </summary>
        public IList<CMSPageSideNavigation> side_navigations { get; set; }

        /// <summary>
        /// Gets or Sets the list of Product Category Search Tags
        /// </summary>
        public IList<CMSSearchTag> ProductCategoryList { get; set; }


        /// <summary>
        /// Gets or sets the list of ComparisonToolProduct objects.
        /// </summary>
        public IList<ComparisonToolProduct> products { get; set; }

        /// <summary>
        /// Gets or sets the products associated with the selected product categories for rendering in the view model
        /// </summary>
        public IList<ComparisonToolProduct> productsForCurrentCategoryToRender { get; set; }

        /// <summary>
        /// Gets or sets the selected product for rendering by the product display view
        /// </summary>
        public ComparisonToolProduct currentProduct { get; set; }

        /// <summary>
        /// Gets or sets the voucher url. This is used by the confirmatino view to transfer the user to the order phase in the Vendor/SME journey
        /// </summary>
        public string VoucherUrl { get; set; }

        /// <summary>
        /// Gets or sets the url to access the product logos.
        /// </summary>
        public string VendorProdLogorUrl { get; set; }

        /// <summary>
        /// Gets or sets the selected product category Ids as selected by the user. Multiple product category ids are spearated by a pipe (|)
        /// </summary>
        public string SelectedProductCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the subheader to be displayed in the Comparison tool product views
        /// </summary>
        public string subheader { get; set; }

        /// <summary>
        /// Gets or sets the product ids selected for comparison. Max 3 products. Multiple product category ids are spearated by a pipe (|)
        /// </summary>
        public string SelectedProductId { get; set; }

        /// <summary>
        /// The URL initiating the call. This is used for the "BACK BUTTON" feature.
        /// </summary>
        public string Referrer { get; set; }

        /// <summary>
        /// Gets or sets the indicator that javascript is enabled in the browser.
        /// </summary>
        public bool JavascriptEnabled { get; set; }

        /// <summary>
        /// Gets the products selected by spliting the SelectedProductId string property using the pipe (|) delimeter. Used by the views
        /// </summary>
        public IList<string> productsSelected
        {
            get { return string.IsNullOrWhiteSpace(SelectedProductId) ? new List<string>() : SelectedProductId.Split(",").ToList(); }

        }

        /// <summary>
        /// Gets the product categories selected by spliting the SelectedProductCategoryId string property using the pipe (|) delimeter. Used by the views
        /// </summary>
        public IList<string> productsCategorySelected
        {
            get { return string.IsNullOrWhiteSpace(SelectedProductCategoryId) ? new List<string>() : SelectedProductCategoryId.Split(",").ToList(); }
        }

        private (List<ComparisonToolProduct>, int?) GetCategoryProducts(string categoryName)
        {
            var categoryId = ProductCategoryList?.Where(t => t.name == categoryName).FirstOrDefault()?.id;
            if (!categoryId.HasValue)
            {
                return (new List<ComparisonToolProduct>(), 0);
            }

            return (products?.Where(item => item.product_type == categoryId.Value).ToList(), categoryId);
        }

        public string pageTitle { get; set; } = "Help to Grow: Digital - Get your software discount";
        public string pagename { get; set; } = "Get your software discount";

        public string CmsBaseUrl { get; set; }

        public bool ShowLayoutNavigation => this.ShowLayoutNavigation();

        public ComparisonToolProductCategoryViewModel ProductCategoryAccountingViewModel
        {
            get
            {
                return GetProductCategoryViewModel(ComparisonToolProductCategory.Accounting, "Digital accounting software");
            }
        }

        public ComparisonToolProductCategoryViewModel ProductCategoryCrmViewModel
        {
            get
            {
                return GetProductCategoryViewModel(ComparisonToolProductCategory.CRM, "Customer Relationship Management (CRM) Software");
            }
        }

        public ComparisonToolProductCategoryViewModel ProductCategoryEcommerceViewModel
        {
            get
            {
                return GetProductCategoryViewModel(ComparisonToolProductCategory.Ecommerce, "eCommerce software");
            }
        }

        private ComparisonToolProductCategoryViewModel GetProductCategoryViewModel(ComparisonToolProductCategory comparisonToolProductCategory, string subHeader)
        {
            var (currentCategoryProducts, categoryId) = GetCategoryProducts($"{comparisonToolProductCategory}".ToLower());
            return new ComparisonToolProductCategoryViewModel
            {
                subheader = subHeader,
                productsForCurrentCategoryToRender = currentCategoryProducts,
                CurrentCategoryId = categoryId,
                CurrentCategoryName = comparisonToolProductCategory.GetType().GetMember(comparisonToolProductCategory.ToString())
                    .First().GetCustomAttribute<DescriptionAttribute>()?.Description,
                VendorProdLogorUrl = this.VendorProdLogorUrl,
                productsSelected = this.productsSelected,
                productsCategorySelected = this.productsCategorySelected,
                SelectedProductCategoryId = this.SelectedProductCategoryId,
                SelectedProductId = this.SelectedProductId
            };
        }

		public string ContentKey { get; set; }

        // SEO properties:
        public string title { get; } = "Help to Grow: Digital - compare eligible business software";
        public string description { get; } = "Compare eligible software available for a financial discount through Help to Grow: Digital and compare costs, features and available support for UK businesses.";
        public string meta { get; } = "going digital, planning, budget";
        public bool? index { get; } = true;
        public bool? follow { get; } = true;
        public CMSPageViewModel CMSPageViewModel { get; set; }
    }
}