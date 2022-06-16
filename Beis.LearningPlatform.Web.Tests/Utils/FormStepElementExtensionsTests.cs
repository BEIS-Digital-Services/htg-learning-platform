namespace Beis.LearningPlatform.Web.Tests.Utils;

public class FormStepElementExtensionsTests
{
    [Test]
    public void ValidateElementCheckBoxGroup_AllUnchecked_ShouldReturnErrors()
    {

        FormStepElement element = new FormStepElement
        {
            controlType = FormDisplayControlType.CheckboxGroup,
            answerOptions = new List<FormAnswerOptionElement>
            {
                new FormAnswerOptionElement
                {
                    controlType = FormDisplayControlType.Checkbox,
                    value = "false",
                    hint = "checkbox 1"
                },

                new FormAnswerOptionElement
                {
                    controlType = FormDisplayControlType.Checkbox,
                    value = "false",
                    hint = "checkbox 2"
                }
            }
        };

        element.ValidateElementCheckBoxGroup(out var errors);

        Assert.IsNotNull(element.validationError);
        Assert.IsNotNull(errors);

    }

    [Test]
    public void ValidateElementCheckBoxGroup_AdditionalInfoEnteredButCheckboxUnchecked_ShouldReturnErrors()
    {

        FormStepElement element = new FormStepElement
        {
            controlType = FormDisplayControlType.CheckboxGroup,
            answerOptions = new List<FormAnswerOptionElement>
            {
                new FormAnswerOptionElement
                {
                    controlType = FormDisplayControlType.Checkbox,
                    additionalInfoRequired = true,
                    additionalInfo = "some info",
                    value = "false",
                    hint = "Something else"

                }
            }
        };

        element.ValidateElementCheckBoxGroup(out var errors);

        Assert.IsNotNull(element.answerOptions[0].validationError);
        Assert.IsNotNull(errors);

    }
}
