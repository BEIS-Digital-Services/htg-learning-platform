using System;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSSearchArticle : IComparable
    {
        public int id { get; set; }
        public DateTime? published_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string articleType { get; set; }
        public CMSSearchArticleDoc Article { get; set; }
        public IList<CMSPageLink> Links { get; set; }
        public IList<CMSPageButton> Buttons { get; set; }
        public CMSPageImage image { get; set; }
        public List<CMSSearchTag> tags { get; set; }

        public int order { get; set; }

        public bool? hide { get; set; }

        // Implement IComparable CompareTo method - provide default sort order using order
        public int CompareTo(object obj)
        {
            var other = (CMSSearchArticle)obj;
            return this.order.CompareTo(other.order);
        }
    }
}