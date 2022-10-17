using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library.DTO
{
    [ExcludeFromCodeCoverage]
    public class FeedbackPageUsefulDto : DtoBase
    {
        public bool IsPageUseful { get; set; }
    }
}