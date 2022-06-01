using System;
using System.Collections.Generic;
using System.Linq;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSSearchArticle : IComparable
    {
        private CMSPageLink _firstLink;

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
        public string ReadingTime { get; set; }

        public int order { get; set; }

        public bool? hide { get; set; }

        public bool HasContent
        {
            get 
            {
                return !string.IsNullOrEmpty(Article?.header);
            }
        }

        
        
        public CMSPageLink FirstLink
        {
            get 
            {
                if (_firstLink == null)
                {
                    _firstLink = Links?.FirstOrDefault();
                }

                return _firstLink;                
            }
        }

        // Implement IComparable CompareTo method - provide default sort order using order
        public int CompareTo(object obj)
        {
            var other = (CMSSearchArticle)obj;
            return this.order.CompareTo(other.order);
        }
    }
}