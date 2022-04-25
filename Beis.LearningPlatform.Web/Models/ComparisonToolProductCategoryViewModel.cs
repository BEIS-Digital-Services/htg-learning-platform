using Beis.LearningPlatform.Web.ComparisonTool.Models;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Models
{
    public class ComparisonToolProductCategoryViewModel
    {
        public string subheader { get; set; }
        public IList<ComparisonToolProduct> productsForCurrentCategoryToRender { get; set; }
        public string VendorProdLogorUrl { get; set; }

        public IList<string> productsCategorySelected { get; internal set; }
        public IList<string> productsSelected { get; internal set; }        
        public string SelectedProductCategoryId { get; internal set; }
        public string SelectedProductId { get; internal set; }

        public int? CurrentCategoryId { get; set; }

        public string CurrentCategoryName { get; internal set; }
    }
}