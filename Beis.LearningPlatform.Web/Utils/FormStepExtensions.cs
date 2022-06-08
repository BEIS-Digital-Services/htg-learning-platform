namespace Beis.LearningPlatform.Web.Utils
{
    /// <summary>
    /// A class that defines extension methods for a Form Step.
    /// </summary>
    public static class FormStepExtensions
    {
        /// <summary>
        /// Clears any errors from an element.
        /// </summary>
        /// <param name="element">A FormStep that is the element to clear errors for.</param>
        public static void ClearErrors(this FormStep element)
        {
            element.elements.ClearErrors();
        }

        /// <summary>
        /// Clears any errors from elements.
        /// </summary>
        /// <param name="elements">An IEnumerable of type FormStep containing the elements to clear errors for.</param>
        public static void ClearErrors(this IEnumerable<FormStep> elements)
        {
            foreach (var element in elements)
                element.ClearErrors();
        }

        /// <summary>
        /// Gets the next Form Step identifier.
        /// </summary>
        /// <param name="formSteps">A List of FormStep that are the form steps.</param>
        /// <param name="currentStepID">An int indicating the identifier of the current step.</param>
        /// <returns>An int indicating the identifier of the next step.</returns>
        public static int GetNextFormStepID(this IList<FormStep> formSteps, int currentStepID)
        {
            int returnValue = currentStepID + 1;
            int totalSteps = formSteps.Count;

            // Check if the next step should be skipped
            for (; returnValue <= totalSteps; returnValue++)
            {
                FormStep nextStep = formSteps.Where(x => x.id == returnValue).First();

                nextStep.skipStep = nextStep.IsSkippableFormStep(formSteps);
                if (!nextStep.skipStep)
                    break;
            }

            return returnValue;
        }

        /// <summary>
        /// Calculates if the specific Form Step is skippable.
        /// </summary>
        /// <param name="formStep">A FormStep that is the element to check.</param>
        /// <param name="formSteps">A List of FormStep that are the Form steps.</param>
        /// <returns>A bool indicating whether the step is skippable.</returns>
        public static bool IsSkippableFormStep(this FormStep formStep, IList<FormStep> formSteps)
        {
            bool returnValue = false;

            if (formStep.skippedByElementStepId.HasValue && formStep.skippedByElementId.HasValue && !string.IsNullOrWhiteSpace(formStep.skipConditionValue))
            {
                string skipConditionValue = formStep.skipConditionValue;
                int skipElementId = formStep.skippedByElementId.Value;
                int skipElementStepId = formStep.skippedByElementStepId.Value;

                var refElement = formSteps[skipElementStepId - 1].elements.First(item => item.id == skipElementId);
                if (refElement != null && !string.IsNullOrWhiteSpace(refElement.value))
                {
                    if (refElement.value.Equals(skipConditionValue, StringComparison.OrdinalIgnoreCase))
                        returnValue = true;
                }
            }

            return returnValue;
        }


        /// <summary>
        /// Loads the answer(s) into a Form Step.
        /// </summary>
        /// <param name="formStep">A FormStep that is the Form Step to load answers into.</param>
        /// <param name="formStepAnswer">A FormStepAnswer that is the answer data to load.</param>
        public static void Load(this FormStep formStep, FormStepAnswer formStepAnswer)
        {
            if (formStepAnswer != null)
            {
                if (formStep.id == formStepAnswer.ID)
                {
                    formStep.skipStep = formStepAnswer.IsSkipped;

                    foreach (var answer in formStepAnswer.Answers)
                    {
                        var element = formStep.elements.Where(e => e.id == answer.ID).FirstOrDefault();
                        if (element != default)
                            element.Load(answer);
                        else
                            throw new ArgumentException("The specified answer data contains a missing answer element", nameof(formStepAnswer));
                    }
                }
                else
                    throw new ArgumentException($"The answer identifier '{formStepAnswer.ID}' does not match this Form Step '{formStep.id}'", nameof(formStepAnswer));
            }
            else
                throw new ArgumentNullException(nameof(formStepAnswer));
        }

        /// <summary>
        /// Saves the answer(s) to this Form Step.
        /// </summary>
        /// <param name="formStep">A FormStep that is the Form Step to save.</param>
        /// <returns>A FormStepAnswer containing the saved answer(s).</returns>
        public static FormStepAnswer Save(this FormStep formStep)
        {
            FormStepElementAnswer[] answers;
            FormStepAnswer returnValue;

            answers = formStep.elements.Select(e => e.Save()).ToArray();
            
            returnValue = new()
            {
                Answers = answers,
                ID = formStep.id,
                IsSkipped = formStep.skipStep
            };

            return returnValue;
        }
    }
}