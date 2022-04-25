using Beis.LearningPlatform.Library.DTO.Base;
using Beis.LearningPlatform.Library.Enums;

namespace Beis.LearningPlatform.Library
{
    /// <summary>
    /// A class that defines a DTO for a Skills results email data.
    /// </summary>
    public class SkilledModuleTwoDto : DtoBase, IEmailDto
    {
        public string Priorities { get; set; }
        public SkilledModuleTwoResultType SkilledModuleTwoResultType { get; set; }
    }
}