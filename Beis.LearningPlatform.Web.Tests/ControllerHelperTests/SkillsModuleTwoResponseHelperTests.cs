namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests
{
    public class SkillsModuleTwoResponseHelperTests
    {
        private SkillsModuleTwoResponseHelper _sut = new();
        
        [Test]
        public async Task Test_ConvertToResultsEmail_ReturnsNoSelectedPriorities()
        {
            var form = new DiagnosticToolForm();
            var result = await _sut.ConvertToResultsEmail(form) as SkilledModuleTwoDto;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Priorities, Is.EqualTo("You have not selected any business priorities"));
            Assert.That(result.SkilledModuleTwoResultType, Is.EqualTo(form.SkilledModuleTwoResultType));
        }

        [Test]
        public async Task Test_ConvertToResultsEmail_ReturnsOneSelectedPriority()
        {
            var form = new DiagnosticToolForm
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
                                    new()
                                    {
                                        ResultPageLabel = "item 1",
                                        value = "true"
                                    }
                                }
                            }
                        }
                    }
                },
            };

            var result = await _sut.ConvertToResultsEmail(form) as SkilledModuleTwoDto;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Priorities, Is.EqualTo("item 1"));
            Assert.That(result.SkilledModuleTwoResultType, Is.EqualTo(form.SkilledModuleTwoResultType));
        }

        [Test]
        public async Task Test_ConvertToResultsEmail_ReturnsMultipleSelectedPriorities()
        {
            var form = new DiagnosticToolForm
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
                                    new()
                                    {
                                        ResultPageLabel = "item 1",
                                        value = "true"
                                    }
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
                                    new()
                                    {
                                        ResultPageLabel = "item 2",
                                        value = "false"
                                    }
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
                                    new()
                                    {
                                        ResultPageLabel = "item 3",
                                        value = "true"
                                    }
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
                                    new()
                                    {
                                        ResultPageLabel = "item 4",
                                        value = "true"
                                    }
                                }
                            }
                        }
                    }
                },
            };

            var result = await _sut.ConvertToResultsEmail(form) as SkilledModuleTwoDto;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Priorities, Is.EqualTo("item 1, item 3 and item 4"));
            Assert.That(result.SkilledModuleTwoResultType, Is.EqualTo(form.SkilledModuleTwoResultType));
        }
    }
}