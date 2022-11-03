using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library.DTO
{
    [ExcludeFromCodeCoverage(Justification = "Basic DTO - only data fields, no functions")]
    public class FeedbackProblemReportDto : DtoBase
    {
        public string WhatIWasDoing { get; set; }
        public string WhatWentWrong { get; set; }
    }
}