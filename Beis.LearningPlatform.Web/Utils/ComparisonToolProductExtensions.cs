namespace Beis.LearningPlatform.Web.Utils
{
    public static class ComparisonToolProductExtensions
    {
        internal static IOrderedEnumerable<ComparisonToolProduct> RandomOrder(this IList<ComparisonToolProduct> products)
        {
            return products.OrderBy(p => Guid.NewGuid());
        }

        public static string GetGaLinkId(this ComparisonToolProduct comparisonToolProduct)
        {
            if (string.IsNullOrWhiteSpace(comparisonToolProduct?.product_name))
            {
                return $"{comparisonToolProduct?.product_id}";
            }

            var gaId = comparisonToolProduct.product_name.Replace(" ", "-").UrlEncode(true);
            return $"{gaId}-{comparisonToolProduct?.product_id}";
        }
    } 
}