using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library.DTO
{
    [ExcludeFromCodeCoverage]
    public class FeedbackProblemReportDto : DtoBase
    {
        public string WhatIWasDoing { get; set; }
        public string WhatWentWrong { get; set; }
    }
}