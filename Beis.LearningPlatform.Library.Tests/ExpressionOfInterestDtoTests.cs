namespace Beis.LearningPlatform.Library.Tests
{
    public class ExpressionOfInterestDtoTests
    {
        [Test]
        public void OptInMarketingEmail_IsFalse_WhenNoUserEmail()
        {
            var test = new ExpressionOfInterestDto();

            Assert.That(test.OptInMarketingEmail, Is.False);
        }
        [Test]
        public void OptInMarketingEmail_IsFalse_WhenEmptyUserEmail()
        {
            var test = new ExpressionOfInterestDto
            {
                UserEmail = ""
            };

            Assert.That(test.OptInMarketingEmail, Is.False);
        }
        
        [Test]
        public void OptInMarketingEmail_IsTrue_WhenHasUserEmail()
        {
            var test = new ExpressionOfInterestDto
            {
                UserEmail = "test@test.com"
            };

            Assert.That(test.OptInMarketingEmail, Is.True);
        }
        
        [Test]
        public void OptInMarketingPhone_IsFalse_WhenNoUserPhone()
        {
            var test = new ExpressionOfInterestDto();

            Assert.That(test.OptInMarketingPhone, Is.False);
        }
        [Test]
        public void OptInMarketingPhone_IsFalse_WhenEmptyUserPhone()
        {
            var test = new ExpressionOfInterestDto
            {
                UserPhone = ""
            };

            Assert.That(test.OptInMarketingPhone, Is.False);
        }
        
        [Test]
        public void OptInMarketingPhone_IsTrue_WhenHasUserPhone()
        {
            var test = new ExpressionOfInterestDto
            {
                UserPhone = "01234 567890"
            };

            Assert.That(test.OptInMarketingPhone, Is.True);
        }
    }
}