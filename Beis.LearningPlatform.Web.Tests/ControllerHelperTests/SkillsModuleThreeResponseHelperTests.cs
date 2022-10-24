namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests
{
    public class SkillsModuleThreeResponseHelperTests
    {
        [Test]
        public async Task Test_ConvertToResultsEmail_ReturnsCorrectOptions()
        {
            var sut = new SkillsModuleThreeResponseHelper();
            var form = new DiagnosticToolForm()
            {
                steps = new List<FormStep>
                {
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new()
                            {
                                answerOptions = new List<FormAnswerOptionElement>
                                {
                                    new() { value = "question 1 start" },
                                    new() { value = "question 1 next" },
                                    new() { value = "question 1 finally" }
                                }
                            }
                        }
                    },
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new()
                            {
                                answerOptions = new List<FormAnswerOptionElement>
                                {
                                    new() { value = "question 2 start" },
                                    new() { value = "question 2 next" },
                                    new() { value = "question 2 finally" }
                                }
                            }
                        }
                    },
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new()
                            {
                                answerOptions = new List<FormAnswerOptionElement>
                                {
                                    new() { value = "question 3 start" },
                                    new() { value = "question 3 next" },
                                    new() { value = "question 3 finally" }
                                }
                            }
                        }
                    }
                },
                userTypeActionPlanSection = "user type action plan section"
            };
            
            var result = await sut.ConvertToResultsEmail(form) as SkilledModuleThreeDto;

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<SkilledModuleThreeDto>());
            
            Assert.That(result.QuestionOneStart, Is.EqualTo("question 1 start"));
            Assert.That(result.QuestionOneNext, Is.EqualTo("question 1 next"));
            Assert.That(result.QuestionOneFinally, Is.EqualTo("question 1 finally"));
            Assert.That(result.QuestionTwoStart, Is.EqualTo("question 2 start"));
            Assert.That(result.QuestionTwoNext, Is.EqualTo("question 2 next"));
            Assert.That(result.QuestionTwoFinally, Is.EqualTo("question 2 finally"));
            Assert.That(result.QuestionThreeStart, Is.EqualTo("question 3 start"));
            Assert.That(result.QuestionThreeNext, Is.EqualTo("question 3 next"));
            Assert.That(result.QuestionThreeFinally, Is.EqualTo("question 3 finally"));
            Assert.That(result.UserTypeActionPlanSection, Is.EqualTo("user type action plan section"));
        } 
    }
}