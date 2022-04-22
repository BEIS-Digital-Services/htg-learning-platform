using Beis.LearningPlatform.Web.ComparisonTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beis.LearningPlatform.Web.Utils
{
    internal static class ComparisonToolProductExtensions
    {
        internal static IOrderedEnumerable<ComparisonToolProduct> RandomOrder(this IList<ComparisonToolProduct> products)
        {
            return products.OrderBy(p => Guid.NewGuid());
        }
    }
}