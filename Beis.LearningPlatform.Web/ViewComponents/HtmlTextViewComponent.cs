namespace Beis.LearningPlatform.Web.ViewComponents
{
    public class HtmlTextViewComponent : ViewComponent
    {
        private readonly IHtmlTextService _htmlTextService;

        public HtmlTextViewComponent(IHtmlTextService htmlTextService)
        {
            _htmlTextService = htmlTextService;
        }

        public IViewComponentResult Invoke(HtmlTextInputViewModel viewModel)
        {
            var cleanedHtmlText = viewModel.Text;

            if (viewModel.ConvertNewline)
            {
                cleanedHtmlText = _htmlTextService.ReplaceLineBreaks(cleanedHtmlText);
            }

            if (viewModel.CleanWhiteSpace)
            {
                cleanedHtmlText = _htmlTextService.CleanWhiteSpace(cleanedHtmlText);
            }

            return View(new HtmlTextViewModel { HtmlText = cleanedHtmlText });
        }
    }
}