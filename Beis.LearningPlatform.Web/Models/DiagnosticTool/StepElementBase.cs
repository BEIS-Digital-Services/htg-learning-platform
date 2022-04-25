using Beis.LearningPlatform.Web.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace Beis.LearningPlatform.Web.Models.DiagnosticTool
{
    /// <summary>
    /// A class that defines the base functionality of a step element for a Diagnostic Tool Form.
    /// </summary>
    public class StepElementBase : IComparable, IComparable<StepElementBase>
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public StepElementBase()
        {
            _myComparableInterface = this;
        }

        private readonly IComparable<StepElementBase> _myComparableInterface;

        public string additionalInfo { get; set; }

        public bool additionalInfoRequired { get; set; }
        public string additionalInfoText { get; set; }

        [Required]
        public FormDisplayControlType controlType { get; set; }

        [MaxLength(255, ErrorMessage = "Form Step Question hint can not exceed 255 characters!")]
        public string hint { get; set; }
        public bool showHintInBold { get; set; }
        public string hint2 { get; set; }
        public bool showHint2InBold { get; set; }

        [Required]
        public int id { get; set; }

        [RequiredIfControlTypeOf("controlType", "List", "validationError", "Please select at least ONE option.")]
        public string selectedValue { get; set; }

        [RequiredIfControlTypeOf("controlType", "Text", "validationError", "Please Specify an Answer.")]
        [MaxLength(255, ErrorMessage = "Form Step Question Text can not exceed 255 characters!")]

        public string strongText { get; set; }
        public string text { get; set; }

        public string text2 { get; set; }
        public string subText { get; set; }
        public string nextButtonText { get; set; }
        public string textControlClass { get; set; }

        [Required]
        public string textControlHtmlId { get; set; }

        [Required]
        public string textControlHtmlName { get; set; }
        public int score { get; set; }

        public string validationError { get; set; }

        [RequiredIfControlTypeOf("controlType", "RadioGroup", "validationError", "Please select at least ONE option.")]
        public string value { get; set; }

        int IComparable.CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (obj is StepElementBase stepElement)
            {
                return _myComparableInterface.CompareTo(stepElement);
            }

            throw new ArgumentException("The specified comparison object is not a valid type", nameof(obj));
        }

        int IComparable<StepElementBase>.CompareTo(StepElementBase other)
        {
            if (other != null)
                return string.Compare(text, other.text);

            throw new ArgumentNullException(nameof(other));
        }
    }
}