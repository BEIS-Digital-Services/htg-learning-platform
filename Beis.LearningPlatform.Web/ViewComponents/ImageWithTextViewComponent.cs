using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class ImageWithTextViewComponent : ViewComponent
    {
        private readonly CmsOption _cmsOption;
        private readonly MarkdownPipeline _markdownPipeline;

        public ImageWithTextViewComponent(IOptions<CmsOption> cmsOptions, MarkdownPipeline markdownPipeline)
        {
            _cmsOption = cmsOptions.Value;
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new ImageWithTextViewModel(cmsPageComponent);
            if (viewModel.HasContent)
            {
                viewModel.ImageUrl = $"{_cmsOption.ApiBaseUrl}{viewModel.Component.image.url}";
                viewModel.ImageAlt = viewModel.Component.image.alternativeText;
                viewModel.HtmlCopy = Markdown.ToHtml(viewModel.Component.copy, _markdownPipeline);
            }
            return View(viewModel);
        }
    }
}