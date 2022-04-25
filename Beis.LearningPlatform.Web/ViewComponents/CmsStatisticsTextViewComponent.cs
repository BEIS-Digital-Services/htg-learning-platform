using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Beis.LearningPlatform.Web.Utils;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsStatisticsTextViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsStatisticsTextViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsStatisticsTextViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.HtmlText = Markdown.ToHtml(viewModel.Component.text, _markdownPipeline);
                viewModel.HtmlStatisticText = Markdown.ToHtml(viewModel.Component.statisticText, _markdownPipeline);

                viewModel.ClassNameBackgroundColour = GetClassName(viewModel.Component.backgroundColor, true);
                viewModel.ClassNameStatisticNumberColour = GetClassName(viewModel.Component.statisticNumberColor);
                viewModel.ClassNameStatisticTextColor = GetClassName(viewModel.Component.statisticTextColor);
                

            }
            return View(viewModel);
        }

        private static string GetClassName(string colourName, bool isBackground = false)
        {
            if (string.IsNullOrWhiteSpace(colourName))
            {
                return colourName;
            }

            var className = CamelCaseConverter.Delimiter(colourName, "-");
            return isBackground ? className : $"{className}-text";
        }
    }
}