using Beis.LearningPlatform.Web.Enums;
using Beis.LearningPlatform.Web.Models;
using System.Collections.Generic;
using Beis.LearningPlatform.Web.Utils;

namespace Beis.LearningPlatform.Web.StrapiApi.Models
{
    public class CMSPageComponent
    {
        public int Index { get; set; }
        public string __component { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string background { get; set; }

        public string AlteredBackground => !string.IsNullOrWhiteSpace(background) ? CamelCaseConverter.Delimiter(background, "-") : string.Empty;

        public string type { get; set; }
        public IList<CMSPageTextblock> textblocks { get; set; }
        public IList<CMSPageButton> buttons { get; set; }
        public IList<CMSPageLink> links { get; set; }
        public IList<CMSPageHeroBanner> hero_banners { get; set; }
        public CMSPageImage image { get; set; }
        public CMSPageContent content { get; set; }
        public string videoLink { get; set; }
        public IList<CMSPageTextblock> textblock { get; set; }

        public string url { get; set; }

        public string AlteredUrl => string.IsNullOrWhiteSpace(url) && media?.Count > 0 ? media[0].url : url;

        public string ImageUrl
        {
            get
            {
                var imageUrl = default(string);
                if (!string.IsNullOrWhiteSpace(abbr))
                {
                    imageUrl = abbr.ToLower() switch
                    {
                        "pdf" => "/assets/images/pdf.svg",
                        "text" => "/assets/images/txt.svg",
                        "csv" => "/assets/images/csv.svg",
                        "word" => "/assets/images/doc.svg",
                        "pp" => "/assets/images/ppt.svg",
                        "excel" => "/assets/images/xls.svg",
                        _ => "/assets/images/txt.svg"
                    };
                }
                return imageUrl;
            }
        }

        public string header { get; set; }
        public string subheader { get; set; }
        public string copy { get; set; }
        public string intro { get; set; }
        public string color { get; set; }
        public string linksAlign { get; set; }
        public string buttonsAlign { get; set; }
        public string Type { get; set; }
        public CMSPageColumn Column1 { get; set; }
        public CMSPageColumn Column2 { get; set; }
        public CMSPageColumn Column3 { get; set; }
        public IList<CMSPageLink> Links1 { get; set; }
        public IList<CMSPageLink> Links2 { get; set; }
        public IList<CMSPageLink> Links3 { get; set; }
        public IList<CMSPageTag> Tags1 { get; set; }
        public IList<CMSPageTag> Tags2 { get; set; }
        public IList<CMSPageTag> Tags3 { get; set; }
        public CMSPageImage Image1 { get; set; }
        public CMSPageImage Image2 { get; set; }
        public CMSPageImage Image3 { get; set; }
        public IList<CMSPageTag> tags { get; set; }
        public IList<CMSPageAuthor> author { get; set; }
        public string topSpace { get; set; }

        public string AlteredPaddingTopSpace => topSpace != "nospace" ? "govuk-!-padding-top-6" : string.Empty;

        public string AlteredMarginTopSpace => topSpace != "nospace" ? "govuk-!-margin-top-6" : string.Empty;

        public string bottomSpace { get; set; }

        public string AlteredPaddingBottomSpace => bottomSpace != "nospace" ? "govuk-!-padding-bottom-6" : string.Empty;

        public string AlteredMarginBottomSpace => bottomSpace != "nospace" ? "govuk-!-margin-bottom-6" : string.Empty;

        public string message { get; set; }
        public string NoHeader { get; set; }
        public string NoCopy { get; set; }
        public string PeportHeader { get; set; }
        public string ReportCopy { get; set; }
        public string ReportLabel1 { get; set; }
        public string ReportLabel2 { get; set; }
        public string NoLabel1 { get; set; }
        public string ReportHeader { get; set; }
        public CMSPageButton titleYes { get; set; }
        public CMSPageButton titleNo { get; set; }
        public CMSPageButton titleReport { get; set; }
        public CMSPageButton ReportSubmitButton { get; set; }
        public CMSPageButton ReportCancelButton { get; set; }
        public CMSPageButton NoSubmitButton { get; set; }
        public CMSPageLink NoEmailAddressLink { get; set; }
        public CMSPageButton NoCancelButton { get; set; }
        public IList<CMSSearchArticle> search_articles { get; set; }
        public bool? hide { get; set; }
        public string title { get; set; }
        public string Ref { get; set; }
        public string abbr { get; set; }
        public IList<CMSPageMedia> media { get; set; }
        public string one { get; set; }
        public string two { get; set; }
        public string three { get; set; }
        public string four { get; set; }
        public IList<CMSPageTableRow> rows { get; set; }
        public string imagePosition { get; set; }
        public string imageSize { get; set; }
        public string aria { get; set; }

        //BusinessCard Component        
        public string businessName { get; set; }
        public string sector { get; set; }
        public string location { get; set; }
        public string businessFounded { get; set; }
        public string visit { get; set; }
        public bool? contactSMEOptional { get; set; }

        //Quote Callout/Column Component
        public AlignmentValue? quotePosition { get; set; }
        public string quote { get; set; }
        public string quoteCalloutAuthor { get; set; }
        public string quoteColumnAuthor { get; set; }
        public string quoteTwoThirdsAuthor { get; set; }
        
        
        //region Social media Component
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string telegram { get; set; }
        public string instagram { get; set; }
        public string shareLink { get; set; }
        public string shareLinkTitle { get; set; }
        public string linkedIn { get; set; }
        
        //region Statistics Text Component
        public string text { get; set; }
        public string statisticText { get; set; }
        public string statisticNumber { get; set; }
        public AlignmentValue? statisticBoxAlignment { get; set; }
        public string backgroundColor { get; set; }
        public string statisticNumberColor { get; set; }
        public string statisticTextColor { get; set; }
        public IList<CMSPageLink> statisticLink { get; set; }
		
		//AdaptiveImage Component component
		public CMSPageImage desktop_image { get; set; }
		public CMSPageImage mobile_image { get; set; }

		//Custom properties
		public IEnumerable<string> SelectedTags { get; internal set; }
        public string SelectedTagsCsv
        {
            get
            {
                if (SelectedTags == null)
                {
                    return default;
                }
                return string.Join(",", SelectedTags);
            }
        }

		// Three levels component
		public string header1 { get; set; }
		public string header2 { get; set; }
		public string header3 { get; set; }
		public string copy1 { get; set; }
		public string copy2 { get; set; }
		public string copy3 { get; set; }

        // three-column-heroes
        public CMSColumnHero ColumnOne { get; set; }
        public CMSColumnHero ColumnTwo { get; set; }
        public CMSColumnHero ColumnThree { get; set; }

        // LinkListHero
        public IList<CMSSimpleLink> HeroLinks { get; set; }

        // LandingPageHeroList
        public List<CmsLandingPageHeroViewModel> Heroes { get; set; }

        // Navigation Breadcrumbs
        public IList<CMSSimpleLink> AdditionalLinks { get; set; }
        public string Summary { get; set; }
        public CMSPageLink Link { get; set; }
        public List<CmsAccordionItemViewModel> AccordionItems { get; set; }

    }
}