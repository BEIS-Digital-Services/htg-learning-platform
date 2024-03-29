﻿using System.Diagnostics.CodeAnalysis;
using Beis.LearningPlatform.Library.Enums;

namespace Beis.LearningPlatform.Library
{
    /// <summary>
    /// A class that defines a DTO for a Skills results email data.
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Basic DTO - only data fields, no functions")]
    public class SkilledModuleTwoDto : DtoBase, IEmailDto
    {
        public string Priorities { get; set; }
        public SkilledModuleTwoResultType SkilledModuleTwoResultType { get; set; }
    }
}