using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests
{
    public class DiagnosticToolEmailResponseHelperTests
    {
        private const string BaseUrl = "https://www.learn-to-grow-your-business.service.gov.uk";
        private readonly IOptions<WebsiteOption> _websiteOptions = ConfigOptions.Create(new WebsiteOption { BaseUrl = BaseUrl });

        [Test]
        public async Task ConvertToResultsEmail_WillPopulateDto()
        {
            var articles = new List<CMSSearchArticle>
            {
                new ()
                {
                    Article = new CMSSearchArticleDoc()
                    {
                        copy = "description",
                        header = "header"
                    }
                },
                new ()
                {
                    Article = new CMSSearchArticleDoc()
                    {
                        copy = "description",
                        subheader = "header"
                    },
                    Links = new List<CMSPageLink>
                    {
                        new()
                        {
                            url = "/article"
                        }
                    }
                }
            };
            
            var form = new DiagnosticToolForm
            {
                Articles = articles,
                steps = new List<FormStep>
                {
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new()
                            {
                                controlType = FormDisplayControlType.RadioGroup,
                                value = "0",
                                answerOptions = new List<FormAnswerOptionElement>
                                {
                                    new ()
                                    {
                                        text = "trade method",
                                        value = "0"
                                    }
                                }
                            }
                        }
                    },
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new() { selectedValue = "business sector" }
                        }
                    },
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new()
                            {
                                controlType = FormDisplayControlType.RadioGroup,
                                value = "2",
                                answerOptions = new List<FormAnswerOptionElement>
                                {
                                    new ()
                                    {
                                        text = "software needs",
                                        value = "2"
                                    }
                                }
                            }
                        }
                    },
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new() { value = "4" }
                        }
                    },
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new() { value = "4" }
                        }
                    },
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new() { value = "question 6 answer" }
                        }
                    },
                    new()
                    {
                        elements = new List<FormStepElement>
                        {
                            new()
                            {
                                controlType = FormDisplayControlType.RadioGroup,
                                value = "6",
                                answerOptions = new List<FormAnswerOptionElement>
                                {
                                    new ()
                                    {
                                        text = "software choices",
                                        value = "6"
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
                                controlType = FormDisplayControlType.RadioGroup,
                                value = "7",
                                answerOptions = new List<FormAnswerOptionElement>
                                {
                                    new ()
                                    {
                                        text = "streamline tasks",
                                        value = "7"
                                    }
                                }
                            }
                        }
                    }
                },
            };
            
            var sut = new DiagnosticToolEmailResponseHelper(_websiteOptions);
            var result = await sut.ConvertToResultsEmail(form) as DiagnosticToolResultsEmailDataDto;

            result.Should().NotBeNull();
            result.TradeMethod.Should().Be("trade method");
            result.BusinessSector.Should().Be("business sector");
            result.SoftwareNeed.Should().Be("software needs");
            result.Question6Answer.Should().Be("question 6 answer");
            result.SoftwareChoices.Should().Be("software choices");
            result.StreamlineTasks.Should().Be("streamline tasks");
            result.RecommendedSoftware.Should().Be("");
            
            result.RelatedArticles.Should().NotBeNull();
            result.RelatedArticles.Length.Should().Be(2);
            
            result.RelatedArticles[0].ArticleURL.Should().Be(BaseUrl);
            result.RelatedArticles[0].Title.Should().Be(articles[0].Article.header + "\n");
            result.RelatedArticles[0].Description.Should().Be(articles[0].Article.copy + "\n");
            
            result.RelatedArticles[1].ArticleURL.Should().Be(BaseUrl + "/article");
            result.RelatedArticles[1].Title.Should().Be(articles[1].Article.subheader + "\n");
            result.RelatedArticles[1].Description.Should().Be(articles[1].Article.copy + "\n");
        }
    }    
}