using Beis.LearningPlatform.Web.Models;
using Beis.LearningPlatform.Web.StrapiApi.Models;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class CmsCardViewComponent : ViewComponent
    {
        private readonly MarkdownPipeline _markdownPipeline;

        public CmsCardViewComponent(MarkdownPipeline markdownPipeline)
        {
            _markdownPipeline = markdownPipeline;
        }

        public IViewComponentResult Invoke(CMSPageComponent cmsPageComponent)
        {
            var viewModel = new CmsCardViewModel(cmsPageComponent);
            viewModel.One = Markdown.ToHtml(viewModel.Component.one, _markdownPipeline);
            viewModel.Two = Markdown.ToHtml(viewModel.Component.two, _markdownPipeline);
            viewModel.Three = Markdown.ToHtml(viewModel.Component.three, _markdownPipeline);
            viewModel.Four = Markdown.ToHtml(viewModel.Component.four, _markdownPipeline);
            return View(viewModel);
        }
    }
}