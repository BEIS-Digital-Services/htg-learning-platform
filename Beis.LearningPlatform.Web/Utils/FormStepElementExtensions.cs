namespace Beis.LearningPlatform.Web.Utils
{
    /// <summary>
    /// A class that defines extension methods for a Form Step Element.
    /// </summary>
    public static class FormStepElementExtensions
    {
        private const string ERROR_MESSAGE = "Answer the question below to continue";
        private const string ERROR_HEADING_TEXTCONTROL = "Form incomplete";
        private static void LoadCheckboxGroup(this FormStepElement element, FormStepElementAnswer answer)
        {
            if (element.controlType == FormDisplayControlType.CheckboxGroup)
            {
                foreach (var item in answer.Answers)
                {
                    if (TryLoadCheckboxGroupAnswer(item, out int id, out bool hasAdditionalInfo, out string additionalInfo))
                    {
                        var answerOption = element.answerOptions.Where(ao => ao.id == id).FirstOrDefault();
                        if (answerOption != default)
                        {
                            answerOption.value = "true";
                            if (hasAdditionalInfo)
                                answerOption.additionalInfo = additionalInfo;
                        }
                        else
                            throw new ArgumentException($"The specified answer contains an invalid answer option '{id}'", nameof(answer));
                    }
                    else
                        throw new ArgumentException("The specified answer contains an invalid CheckboxGroup answer", nameof(answer));
                }
            }
            else
                throw new ArgumentException("The specified element is not a CheckboxGroup control type", nameof(element));
        }

        private static void LoadList(this FormStepElement element, FormStepElementAnswer answer)
        {
            if (element.controlType == FormDisplayControlType.List)
            {
                if (answer.Answers.Length == 1)
                    element.selectedValue = answer.Answers[0];
            }
            else
                throw new ArgumentException("The specified element is not a List control type", nameof(element));
        }

        private static void LoadRadioGroup(this FormStepElement element, FormStepElementAnswer answer)
        {
            if (element.controlType == FormDisplayControlType.RadioGroup)
            {
                if (answer.Answers.Length == 1)
                    element.value = answer.Answers[0];
            }
            else
                throw new ArgumentException("The specified element is not a RadioGroup control type", nameof(element));
        }

        private static string[] SaveCheckboxGroup(this FormStepElement element)
        {
            string[] returnValue;

            if (element.controlType == FormDisplayControlType.CheckboxGroup)
            {
                var results = element.answerOptions.Where(ao => ao.IsSelected() && !ao.additionalInfoRequired).Select(ao => $"{ao.id}");
                results = element.answerOptions.Where(ao => ao.IsSelected() && ao.additionalInfoRequired).Select(ao => $"{ao.id}|true|{ao.additionalInfo}").Concat(results);
                returnValue = results.ToArray();
            }
            else
                throw new ArgumentException("The specified element is not a CheckboxGroup control type", nameof(element));

            return returnValue;
        }

        private static string[] SaveList(this FormStepElement element)
        {
            if (element.controlType == FormDisplayControlType.List)
                return new[] { element.selectedValue };
            else
                throw new ArgumentException("The specified element is not a List control type", nameof(element));
        }

        private static string[] SaveRadioGroup(this FormStepElement element)
        {
            if (element.controlType == FormDisplayControlType.RadioGroup)
                return new[] { element.value };
            else
                throw new ArgumentException("The specified element is not a RadioGroup control type", nameof(element));
        }

        private static bool TryLoadCheckboxGroupAnswer(string answerData, out int id, out bool hasAdditionalInfo, out string additionalInfo)
        {
            bool returnValue = false;

            // Defaults
            additionalInfo = default;
            hasAdditionalInfo = false;
            id = 0;

            if (answerData.Contains('|'))
            {
                string[] parts = answerData.Split('|');
                if (parts?.Length == 3 && int.TryParse(parts[0], out id))
                {
                    additionalInfo = parts[2];
                    hasAdditionalInfo = true;
                    returnValue = true;
                }
            }
            else
            {
                if (int.TryParse(answerData, out id))
                    returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Clears any errors from an element.
        /// </summary>
        /// <param name="element">A FormStepElement that is the element to clear errors for.</param>
        public static void ClearErrors(this FormStepElement element)
        {
            element.validationError = string.Empty;
            element.answerOptions.ClearErrors();
        }

        /// <summary>
        /// Clears any errors from elements.
        /// </summary>
        /// <param name="elements">An IEnumerable of type FormStepElement containing the elements to clear errors for.</param>
        public static void ClearErrors(this IEnumerable<FormStepElement> elements)
        {
            foreach (var element in elements)
                element.ClearErrors();
        }

        /// <summary>
        /// Gets an array of selected answer options.
        /// </summary>
        /// <param name="element">A FormStepElement that is the element to get the selected answer options from.</param>
        /// <returns>An array of FormAnswerOptionElement containing the selected answer options.</returns>
        public static FormAnswerOptionElement[] GetSelectedAnswerOptions(this FormStepElement element)
        {
            FormAnswerOptionElement[] returnValue = default;

            if (element.answerOptions?.Count > 0)
            {
                if (element.controlType == FormDisplayControlType.CheckboxGroup)
                {
                    // Get all checked items from checkbox list
                    returnValue = element.answerOptions.Where(x => x.value.Equals("true", StringComparison.OrdinalIgnoreCase)).ToArray();
                }
                else if (element.controlType == FormDisplayControlType.RadioGroup)
                {
                    // Get singular item from radio group list
                    returnValue = element.answerOptions.Where(x => x.value == element.value).ToArray();
                }
                else
                    throw new ArgumentException("The specified Form Step Element is not a valid control type", nameof(element));
            }


            return returnValue.ToArray();
        }

        /// <summary>
        /// Gets a string containing a list of selected answer options.
        /// </summary>
        /// <param name="element">A FormStepElement that is the element to get the selected answer options from.</param>
        /// <param name="separator">A string containing the separator to use to separate the elements.</param>
        /// <param name="finalSeparator">A string containing the separator to use between the last two elements.</param>
        /// <param name="useAdditionalInfo">A bool indicating whether to use the additional information if present as opposed to the result page label.</param>
        /// <returns>A string the selected answer options.</returns>
        public static string GetSelectedAnswerOptionsAsString(this FormStepElement element, string separator = ", ", string finalSeparator = " and ", bool useAdditionalInfo = true)
        {
            List<string> items = new();
            string returnValue;

            var selectedAnswers = element.GetSelectedAnswerOptions();
            foreach (var answer in selectedAnswers)
            {
                if (useAdditionalInfo && !string.IsNullOrWhiteSpace(answer.additionalInfo))
                    items.Add(answer.additionalInfo.Trim());
                else if (!string.IsNullOrWhiteSpace(answer.ResultPageLabel))
                    items.Add(answer.ResultPageLabel.Trim());
                else
                    items.Add(answer.text.Trim());
            }

            returnValue = items.ToArray().JoinToSeparatedList();

            return returnValue;
        }

        /// <summary>
        /// Loads the answer(s) into the element.
        /// </summary>
        /// <param name="element">A FormStepElement that is the element to load into.</param>
        /// <param name="answer">A FormStepElementAnswer that is the answer to load.</param>
        public static void Load(this FormStepElement element, FormStepElementAnswer answer)
        {
            switch (element.controlType)
            {
                case FormDisplayControlType.CheckboxGroup:
                    element.LoadCheckboxGroup(answer);
                    break;

                case FormDisplayControlType.List:
                    element.LoadList(answer);
                    break;

                case FormDisplayControlType.RadioGroup:
                    element.LoadRadioGroup(answer);
                    break;

                default:
                    throw new ArgumentException($"The control type '{element.controlType}' cannot be loaded", nameof(answer));
            }
        }

        /// <summary>
        /// Saves the answer(s) to this element.
        /// </summary>
        /// <param name="element">A FormStepElement that is the element to save.</param>
        /// <returns>A FormStepElementAnswer containing the saved answer(s).</returns>
        public static FormStepElementAnswer Save(this FormStepElement element)
        {
            string[] answers = element.controlType switch
            {
                FormDisplayControlType.CheckboxGroup => element.SaveCheckboxGroup(),
                FormDisplayControlType.List => element.SaveList(),
                FormDisplayControlType.RadioGroup => element.SaveRadioGroup(),
                _ => throw new ArgumentException($"The control type '{element.controlType}' does not support save operations", nameof(element))
            };

            return new()
            {
                ID = element.id,
                Answers = answers
            };
        }

        /// <summary>
        /// Validates a Form Step Element to ensure the values for the answers are valid.
        /// </summary>
        /// <param name="element">A FormStepElement that is the element to validate.</param>
        /// <param name="validationErrors">An array of FormValidationError that will be set to any validation errors.</param>
        /// <returns>A bool indicating whether the element was valid.</returns>
        public static bool ValidateElement(this FormStepElement element, out FormValidationError[] validationErrors)
        {
            bool returnValue = true;

            // Defaults
            validationErrors = default;

            // Validate element
            if (element.controlType == FormDisplayControlType.CheckboxGroup)
            {
                if (!element.ValidateElementCheckBoxGroup(out validationErrors))
                    returnValue = false;
            }
            else if (element.controlType == FormDisplayControlType.List)
            {
                if (!element.ValidateElementList(out validationErrors))
                    returnValue = false;
            }
            else if (element.controlType == FormDisplayControlType.RadioGroup)
            {
                if (!element.ValidateElementRadioGroup(out validationErrors))
                    returnValue = false;
            }
            else if (element.controlType == FormDisplayControlType.Textarea)
            {
                if (!element.ValidateTextarea(out validationErrors))
                    returnValue = false;
            }
            else
            {
                List<FormValidationError> validationErrorsList = new();

                // Validate suitable child elements
                foreach (var answer in element.answerOptions.Where(x => x.controlType == FormDisplayControlType.Text || x.controlType == FormDisplayControlType.Textarea))
                {
                    if (answer.controlType == FormDisplayControlType.Textarea)
                    {
                        if (!answer.ValidateTextareaControl(out string errorMessage))
                        {
                            element.childHasErrors = true;
                            validationErrorsList.Add(new FormValidationError() { errorMessage = errorMessage, errorHeading = ERROR_HEADING_TEXTCONTROL });
                        }
                    }
                    else
                    {
                        if (!answer.ValidateTextControl(out string errorMessage))
                        {
                            element.childHasErrors = true;
                            validationErrorsList.Add(new FormValidationError() { errorMessage = errorMessage });
                        }
                    }
                }

                if (validationErrorsList.Count > 0)
                {
                    returnValue = false;
                    validationErrors = validationErrorsList.ToArray();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Validates a CheckboxGroup Form Step Element to ensure the values for the answers are valid.
        /// </summary>
        /// <param name="element">A FormStepElement that is the element to validate.</param>
        /// <param name="validationErrors">An array of FormValidationError that will be set to any validation errors.</param>
        /// <returns>A bool indicating whether the element was valid.</returns>
        public static bool ValidateElementCheckBoxGroup(this FormStepElement element, out FormValidationError[] validationErrors)
        {
            var childIsSelected = false;
            string errorMessage = default;
            bool returnValue = true;
            List<FormValidationError> validationErrorsList = new();

            // Defaults
            validationErrors = default;

            if (element.controlType == FormDisplayControlType.CheckboxGroup)
            {
                // Validate children
                foreach (var answer in element.answerOptions.Where(x => x.controlType == FormDisplayControlType.Checkbox))
                {
                    var checkboxIsValid = answer.ValidateCheckBoxControl(out bool isSelected, out errorMessage);

                    if (isSelected)
                        childIsSelected = true;

                    if (!checkboxIsValid)
                    {
                        element.childHasErrors = true;
                        validationErrorsList.Add(new FormValidationError() { errorMessage = errorMessage });
                    }
                }

                // Check for no selected children
                if (!childIsSelected)
                {
                    element.childHasErrors = true;
                    element.validationError = ERROR_MESSAGE;
                    validationErrorsList.Add(new FormValidationError() { errorMessage = element.validationError });
                }
            }
            else
                throw new ArgumentException("The specified element is not a CheckboxGroup-type", nameof(element));

            // Output any errors
            if (validationErrorsList.Count > 0)
            {
                returnValue = false;
                validationErrors = validationErrorsList.ToArray();
            }

            return returnValue;
        }

        /// <summary>
        /// Validates a List Form Step Element to ensure the values for the answers are valid.
        /// </summary>
        /// <param name="element">A FormStepElement that is the element to validate.</param>
        /// <param name="validationErrors">An array of FormValidationError that will be set to any validation errors.</param>
        /// <returns>A bool indicating whether the element was valid.</returns>
        public static bool ValidateElementList(this FormStepElement element, out FormValidationError[] validationErrors)
        {
            bool returnValue = true;

            // Defaults
            validationErrors = default;

            if (element.controlType == FormDisplayControlType.List)
            {
                if (!string.IsNullOrWhiteSpace(element.validationError))
                {
                    returnValue = false;

                    element.childHasErrors = true;
                    element.validationError = ERROR_MESSAGE;
                    validationErrors = new FormValidationError[]
                    {
                        new FormValidationError() { errorMessage = element.validationError }
                    };
                }
            }
            else
                throw new ArgumentException("The specified element is not a List-type", nameof(element));

            return returnValue;
        }

        /// <summary>
        /// Validates a RadioGroup Form Step Element to ensure the values for the answers are valid.
        /// </summary>
        /// <param name="element">A FormStepElement that is the element to validate.</param>
        /// <param name="validationErrors">An array of FormValidationError that will be set to any validation errors.</param>
        /// <returns>A bool indicating whether the element was valid.</returns>
        public static bool ValidateElementRadioGroup(this FormStepElement element, out FormValidationError[] validationErrors)
        {
            var childIsSelected = false;
            string errorMessage = default;
            bool returnValue = true;
            List<FormValidationError> validationErrorsList = new();

            // Defaults
            validationErrors = default;

            if (element.controlType == FormDisplayControlType.RadioGroup)
            {
                // Validate children
                foreach (var answer in element.answerOptions.Where(x => x.controlType == FormDisplayControlType.Radio))
                {
                    if (!string.IsNullOrWhiteSpace(element.value))
                        childIsSelected = true;

                    if (!answer.ValidateRadioControl(out errorMessage))
                    {
                        element.childHasErrors = true;
                        validationErrorsList.Add(new FormValidationError() { errorMessage = errorMessage });
                    }
                }

                // Check for no selected child
                if (!childIsSelected)
                {
                    element.childHasErrors = true;
                    element.validationError = ERROR_MESSAGE;
                    validationErrorsList.Add(new FormValidationError() { errorMessage = element.validationError });
                }
            }
            else
                throw new ArgumentException("The specified element is not a RadioGroup-type", nameof(element));

            // Output any errors
            if (validationErrorsList.Count > 0)
            {
                returnValue = false;
                validationErrors = validationErrorsList.ToArray();
            }

            return returnValue;
        }


        private static bool ValidateTextarea(this FormStepElement element, out FormValidationError[] validationErrors)
        {
            //var childIsSelected = false;
            //string errorMessage = default;
            bool returnValue = true;
            List<FormValidationError> validationErrorsList = new();

            // Defaults
            validationErrors = default;

            if (element.controlType == FormDisplayControlType.Textarea)
            {
                if (string.IsNullOrEmpty(element.value))
                {
                    element.childHasErrors = true;
                    element.validationError = ERROR_MESSAGE;
                    validationErrorsList.Add(new FormValidationError
                    {
                        errorMessage = element.validationError,
                        errorHeading = ERROR_HEADING_TEXTCONTROL
                    });
                }
            }
            else
                throw new ArgumentException("The specified element is not a Textarea-type", nameof(element));

            // Output any errors
            if (validationErrorsList.Count > 0)
            {
                returnValue = false;
                validationErrors = validationErrorsList.ToArray();
            }

            return returnValue;
        }

        /// <summary>
        /// Validates Form Step Elements to ensure the values for the answers are valid.
        /// </summary>
        /// <param name="elements">An IEnumerable of type FormStepElement containing the elements to validate.</param>
        /// <param name="validationErrors">An array of FormValidationError that will be set to any validation errors.</param>
        /// <returns>A bool indicating whether the element was valid.</returns>
        public static bool ValidateElements(this IEnumerable<FormStepElement> elements, out FormValidationError[] validationErrors)
        {
            bool returnValue = true;
            List<FormValidationError> validationErrorsList = new();

            // Defaults
            validationErrors = default;

            // Validate any elements
            foreach (var element in elements)
            {
                if (!element.ValidateElement(out var elementValidationErrors))
                    validationErrorsList.AddRange(elementValidationErrors);
            }

            // See if there were any validation failures
            if (validationErrorsList.Count > 0)
            {
                returnValue = false;
                validationErrors = validationErrorsList.ToArray();
            }

            return returnValue;
        }
    }
}