using AutoMapper;
using Beis.LearningPlatform.BL.DependencyInjection;
using NUnit.Framework;

namespace Beis.LearningPlatform.BL.Tests
{
    public class DependencyInjectionTests
    {
        [Test]
        public void Should_Have_Valid_DiagnosticToolResultsEmailDataProfile()
        {
           var config = new MapperConfiguration(cfg => cfg.AddProfile<DiagnosticToolResultsEmailDataProfile>());
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
    }
}