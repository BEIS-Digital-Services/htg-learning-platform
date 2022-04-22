using Markdig;

namespace Beis.LearningPlatform.Web.Tests.ViewComponentTests
{
    public class BaseViewComponentTest
    {
        protected static MarkdownPipeline GetMarkdownPipeline()
        {
            return new MarkdownPipelineBuilder().UseAdvancedExtensions().UseBootstrap().Build();
        }
    }
}