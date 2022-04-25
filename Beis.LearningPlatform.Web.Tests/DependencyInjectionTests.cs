using AutoMapper;
using Beis.LearningPlatform.Web.DependencyInjection;
using NUnit.Framework;

namespace Beis.LearningPlatform.Web.Tests
{
    public class DependencyInjectionTests
    {
        [Test]
        public void Should_Have_Valid_ComparisonToolProductViewModelProfile()
        {
           var config = new MapperConfiguration(cfg => cfg.AddProfile<ComparisonToolProductProfile>());
            config.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Have_Valid_DiagnosticToolEmailAnswerProfile()
        {
           var config = new MapperConfiguration(cfg => cfg.AddProfile<DiagnosticToolEmailAnswerProfile>());
            config.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Have_Valid_FeedbackPageUsefulProfile()
        {
           var config = new MapperConfiguration(cfg => cfg.AddProfile<FeedbackPageUsefulProfile>());
            config.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Have_Valid_FeedbackProblemReportProfile()
        {
           var config = new MapperConfiguration(cfg => cfg.AddProfile<FeedbackProblemReportProfile>());
            config.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Have_Valid_SatisfactionSurveyDataProfile()
        {
           var config = new MapperConfiguration(cfg => cfg.AddProfile<SatisfactionSurveyDataProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}