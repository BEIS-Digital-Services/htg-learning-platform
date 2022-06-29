namespace Beis.LearningPlatform.Web.Interfaces
{
    public interface IComparisonToolService
    {
        /// <summary>
        /// Gets the list of ALL Products (regardless of product status or vendor status) from the Vendor ProductRepository
        /// </summary>
        /// <returns>List of ComparisonToolProduct objects</returns>
        public Task<IList<ComparisonToolProduct>> GetProducts();

        /// <summary>
        /// Gets the list of ALL APPROVED Products from APPROVED vendors from the Vendor ProductRepository
        /// </summary>
        /// <returns>List of ComparisonToolProduct objects</returns>
        public Task<IList<ComparisonToolProduct>> GetApprovedProductsFromApprovedVendors();

        /// <summary>
        /// Populates the child attributes of the items in the List of ComparisonToolProduct from the Vendor Repositorie.
        /// </summary>
        /// <param name="products">The List of ComparisonToolProduct objects to populate</param>
        public Task PopulateChildRelationships(IList<ComparisonToolProduct> products);

        /// <summary>
        /// Sets the Nav and Footer CMS components for an instatiated ComparisonToolPageViewModel object using ICMSService
        /// </summary>
        /// <param name="comparisonToolPage">The ComparisonToolPageViewModel object</param>
        /// <returns>True if successful, false if unsuccessful</returns>
        public Task<bool> SetNavAndFooter(ComparisonToolPageViewModel comparisonToolPage);

        public Task<ComparisonToolProduct> GetProduct(long productId);
        public Task<ComparisonToolProduct> GetApprovedProductFromApprovedVendor(long productId);
    }
}