using System.Diagnostics.CodeAnalysis;

namespace Beis.LearningPlatform.Library
{
    [ExcludeFromCodeCoverage]
    public class SatisfactionSurveyDto : DtoBase
    {
        public string Rating { get; set; }
        public string Comment { get; set; }
    }
}