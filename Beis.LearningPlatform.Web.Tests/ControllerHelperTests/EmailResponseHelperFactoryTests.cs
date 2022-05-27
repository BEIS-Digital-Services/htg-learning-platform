using Beis.LearningPlatform.Web.ControllerHelpers;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Tests.ControllerHelperTests
{
    public class EmailResponseHelperFactoryTests
    {
        private EmailResponseHelperFactory _emailResponseHelperFactory;
        [SetUp]
        public void Setup()
        {
            IEnumerable<IEmailResponseHelper> emailResponseHelpers = new IEmailResponseHelper[] { new SkillsEmailResponseHelper(),
                                                                    new SkillsModuleTwoResponseHelper(), new SkillsModuleThreeResponseHelper() };

            _emailResponseHelperFactory = new EmailResponseHelperFactory(emailResponseHelpers);
        }

        [TestCase(FormTypes.SkillsOne)]
        [TestCase(FormTypes.SkillsTwo)]
        public void Should_return_right_helper(FormTypes formType)
        {
            var helper = _emailResponseHelperFactory.Get(formType);

            helper.Should().NotBeNull();
            helper.FormType.Should().Be(formType);

        }

        [TestCase(FormTypes.SkillsThreeNewcomerPlanning)]
        [TestCase(FormTypes.SkillsThreeNewcomerCommunication)]
        [TestCase(FormTypes.SkillsThreeNewcomerSupport)]
        [TestCase(FormTypes.SkillsThreeNewcomerTesting)]
        [TestCase(FormTypes.SkillsThreeNewcomerTraining)]
        [TestCase(FormTypes.SkillsThreeMoverPlanning)]
        [TestCase(FormTypes.SkillsThreeMoverCommunication)]
        [TestCase(FormTypes.SkillsThreeMoverSupport)]
        [TestCase(FormTypes.SkillsThreeMoverTraining)]
        [TestCase(FormTypes.SkillsThreeMoverTesting)]
        [TestCase(FormTypes.SkillsThreePerformerPlanning)]
        [TestCase(FormTypes.SkillsThreePerformerCommunication)]
        [TestCase(FormTypes.SkillsThreePerformerSupport)]
        [TestCase(FormTypes.SkillsThreePerformerTraining)]
        [TestCase(FormTypes.SkillsThreePerformerTesting)]
        public void Should_return_right_helper_with_flags(FormTypes formType)
        {
            var helper = _emailResponseHelperFactory.Get(formType);

            var result = helper.FormType.HasFlag(formType);

            helper.Should().NotBeNull();
            result.Should().BeTrue();
            
        }
    }
}
