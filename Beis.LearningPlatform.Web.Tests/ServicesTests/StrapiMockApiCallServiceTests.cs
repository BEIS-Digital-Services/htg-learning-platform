namespace Beis.LearningPlatform.Web.Tests.ServicesTests
{
    // NOTE: Check for SocketException: No such host is known to ensure strapi host exist
    public class StrapiMockApiCallServiceTests
    {
        private StrapiMakeApiCallMockService _MakeApiCallService;

        [SetUp]
        public void Setup()
        {
            _MakeApiCallService = new StrapiMakeApiCallMockService();
        }
    }
}