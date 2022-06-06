using Beis.LearningPlatform.Web.Models.DiagnosticTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Utils
{
    /// <summary>
    /// A class that defines extension methods to a Form Answer Option Element.
    /// </summary>
    public static class FormAnswerOptionElementExtensions
    {
        static FormAnswerOptionElementExtensions()
        {
            _selectableControls = new()
            {
                { FormDisplayControlType.Checkbox, (answerOption) => answerOption.value.Equals("true") },
                { FormDisplayControlType.ListItem, (answerOption) => answerOption.value.Equals(answerOption.parent?.selectedValue) },
                { FormDisplayControlType.Radio, (answerOption) => answerOption.value.Equals(answerOption.parent?.value) },
                { FormDisplayControlType.Text, (answerOption) => !string.IsNullOrWhiteSpace(answerOption.value) }
            };
        }

        private readonly static Dictionary<FormDisplayControlType, Func<FormAnswerOptionElement, bool>> _selectableControls;

        /// <summary>
        /// Clears any errors from an element.
        /// </summary>
        /// <param name="element">A FormAnswerOptionElement that is the element to clear errors for.</param>
        public static void ClearErrors(this FormAnswerOptionElement element)
        {
            element.validationError = string.Empty;
        }

        /// <summary>
        /// Clears any errors from elements.
        /// </summary>
        /// <param name="elements">An IEnumerable of type FormAnswerOptionElement containing the elements to clear errors for.</param>
        public static void ClearErrors(this IEnumerable<FormAnswerOptionElement> elements)
        {
            foreach (var answer in elements)
                answer.ClearErrors();
        }

        /// <summary>
        /// Gets a list of search tag display names for the specified Form Answer Option Elements.
        /// </summary>
        /// <param name="elements">An array of FormAnswerOptionElement that are the elements to get the list of search tags from.</param>
        /// <returns>A Task representing the asynchronous operation.  An array of string containing the list of search tags.</returns>
        public static async Task<string[]> GetSearchTagDisplayNames(this FormAnswerOptionElement[] elements)
        {
            List<string> returnValue = new();

            var searchTags = elements.Where(x => x.searchTags != null && x.searchTags.Count > 0)
                                     .Select(x => x.searchTags.FirstOrDefault()).Distinct().ToArray();
            foreach (var searchTag in searchTags)
                returnValue.Add(await searchTag.DisplayName(searchTag.ToString()));

            return returnValue
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }

        /// <summary>
        /// Gets a list of search tag display names for the specified Form Answer Option Elements.
        /// </summary>
        /// <param name="elements">An array of FormAnswerOptionElement that are the elements to get the list of search tags from.</param>
        /// <param name="separator">A string containing the separator to use to separate the elements.</param>
        /// <param name="finalSeparator">A string containing the separator to use between the last two elements.</param>
        /// <returns>A Task representing the asynchronous operation.  A string containing the list of search tags.</returns>
        public static async Task<string> GetSearchTagDisplayNamesAsString(this FormAnswerOptionElement[] elements, string separator = ", ", string finalSeparator = " and ")
        {
            string returnValue;

            var searchTagDisplayNames = await elements.GetSearchTagDisplayNames();
            returnValue = searchTagDisplayNames.JoinToSeparatedList(separator, finalSeparator);

            return returnValue;
        }

        /// <summary>
        /// Gets an indication of whether the element has been selected and is valid.
        /// </summary>
        /// <param name="element">A FormAnswerOptionElement that is the element to check.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool IsSelected(this FormAnswerOptionElement element)
        {
            bool returnValue = false;

            if (_selectableControls.ContainsKey(element.controlType) && !string.IsNullOrWhiteSpace(element.value))
                returnValue = _selectableControls[element.controlType](element);

            return returnValue;
        }

        /// <summary>
        /// Validates a Checkbox control to see whether it contains a valid value.
        /// </summary>
        /// <param name="element">A FormAnswerOptionElement that is the control to validate.</param>
        /// <param name="isSelected">A bool that will be set to an indication that a value has been selected.</param>
        /// <param name="errorMessage">A string that will be set to any error associated with the validation.</param>
        /// <returns>A bool indicating whether the control is valid.</returns>
        /// <exception cref="System.ArgumentException">Thrown when the specified element is not a Checkbox control.</exception>
        public static bool ValidateCheckBoxControl(this FormAnswerOptionElement element, out bool isSelected, out string errorMessage)
        {
            bool returnValue = true;

            // Defaults
            errorMessage = default;
            isSelected = false;

            if (element.controlType == FormDisplayControlType.Checkbox)
            {
                // If the answer is selected, process the values, if not, reset the additional info values
                if (element.value.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    isSelected = true;

                    if (element.parent != null)
                    {
                        element.parent.value = element.parent.value + "|" + element.text;
                        errorMessage = string.IsNullOrWhiteSpace(element.hint) ? element.parent.text : element.hint;
                    }
                    else
                        errorMessage = string.IsNullOrWhiteSpace(element.hint) ? string.Empty : element.hint;

                    // Check for additional info checkbox with unset value
                    if (element.additionalInfoRequired && string.IsNullOrWhiteSpace(element.additionalInfo))
                    {
                        returnValue = false;
                        element.validationError = "Additional Information is required for this answer";
                        errorMessage += $" {element.validationError}";
                    }
                }
                else
                    element.additionalInfo = string.Empty;
            }
            else
                throw new ArgumentException("The specified element is not a Checkbox-type", nameof(element));

            return returnValue;
        }

        /// <summary>
        /// Validates a Radio control to see whether it contains a valid value.
        /// </summary>
        /// <param name="element">A FormAnswerOptionElement that is the control to validate.</param>
        /// <param name="errorMessage">A string that will be set to any error associated with the validation.</param>
        /// <returns>A bool indicating whether the control is valid.</returns>
        /// <exception cref="System.ArgumentException">Thrown when the specified element is not a Radio control.</exception>
        public static bool ValidateRadioControl(this FormAnswerOptionElement element, out string errorMessage)
        {
            bool returnValue = true;

            // Defaults
            errorMessage = default;

            if (element.controlType == FormDisplayControlType.Radio)
            {
                if (element.parent != null)
                {
                    // Check if this element requires additional information
                    if (element.additionalInfoRequired)
                    {
                        // Check if this is the selected radio button
                        if (element.value == element.parent.value)
                        {
                            // Validate additional information has been entered
                            if (string.IsNullOrWhiteSpace(element.additionalInfo))
                            {
                                element.additionalInfo = null;
                                element.validationError = "Additional Information is required for this answer";
                                errorMessage = string.IsNullOrWhiteSpace(element.hint) ? element.parent.text : element.hint;
                                errorMessage += $" {element.validationError}";
                            }
                        }
                        else
                        {
                            // Temp Fix: The radio button needs to reference it's parent in order to validate
                            // By default, it will generate an error even if it is not selected
                            // Remove the model state error associated
                            element.additionalInfo = " ";
                        }
                    }
                }
            }
            else
                throw new ArgumentException("The specified element is not a Checkbox-type", nameof(element));

            return returnValue;
        }

        /// <summary>
        /// Validates a Text control to see whether it contains a valid value.
        /// </summary>
        /// <param name="element">A FormAnswerOptionElement that is the control to validate.</param>
        /// <param name="errorMessage">A string that will be set to any error associated with the validation.</param>
        /// <returns>A bool indicating whether the control is valid.</returns>
        /// <exception cref="System.ArgumentException">Thrown when the specified element is not a Text control.</exception>
        public static bool ValidateTextControl(this FormAnswerOptionElement element, out string errorMessage)
        {
            bool returnValue = true;

            // Defaults
            errorMessage = default;

            if (element.controlType == FormDisplayControlType.Text)
            {
                if (!string.IsNullOrWhiteSpace(element.validationError))
                {
                    returnValue = false;
                    if (element.parent != null)
                    {
                        errorMessage = string.IsNullOrWhiteSpace(element.parent.text) ? element.parent.value : element.parent.text;
                        errorMessage += $" {element.validationError}";
                    }
                    else
                        errorMessage = element.validationError;
                }
            }
            else
                throw new ArgumentException("The specified element is not a Text-type", nameof(element));

            return returnValue;
        }

        public static bool ValidateTextareaControl(this FormAnswerOptionElement element, out string errorMessage)
        {
            bool returnValue = true;

            // Defaults
            errorMessage = default;

            if (element.controlType == FormDisplayControlType.Textarea)
            {
                if (string.IsNullOrEmpty(element.value))
                {
                    returnValue = false;
                    errorMessage = "Answer the question below to continue";
                    element.validationError = errorMessage;
                }
            }
            else
                throw new ArgumentException("The specified element is not a Textarea-type", nameof(element));

            return returnValue;
        }
    }
}