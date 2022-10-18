using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library.DTO
{
    [ExcludeFromCodeCoverage(Justification = "Basic DTO - only data fields, no functions")]
    public class FeedbackPageUsefulDto : DtoBase
    {
        public bool IsPageUseful { get; set; }
    }
}