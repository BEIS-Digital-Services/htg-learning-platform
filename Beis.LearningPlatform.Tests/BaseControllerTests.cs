using AutoFixture;
using Beis.LearningPlatform.Data;
using Beis.LearningPlatform.Data.Entities.SatisfactionSurvey;
using Beis.LearningPlatform.Web.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NUnit.Framework.Constraints;

namespace Beis.LearningPlatform.Tests
{
    public abstract class BaseControllerTests
    {
        protected Mock<DataContext> MockLpDbContext { get; }

        protected Mock<HttpContext> MockHttpContext { get; }

        protected ServiceProvider ServiceProvider { get; }

        protected Fixture AutoFixture { get; }

        protected BaseControllerTests()
        {
            MockHttpContext = new Mock<HttpContext>();
            MockLpDbContext = new Mock<DataContext>();
            AutoFixture = new Fixture();
            AutoFixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"HttpListenerConfig:UseSSL", "false"},
                    {"CmsConfig:ApiBaseUrl", "https://www.cmsurl.com"},
                    {"DatabaseConfig:LearningPlatformDbConnectionString", "LearningPlatformDbConnectionString"},
                    {"DatabaseConfig:HelpToGrowDbConnectionString", "HelpToGrowDbConnectionString"},
                })
                .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterAllServices(configuration, false);
            serviceCollection.AddScoped(options => MockLpDbContext.Object);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        protected void SetHttpContext(ControllerContext controllerContext)
        {
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(x => x.Scheme).Returns("http");
            mockHttpRequest.Setup(x => x.Host).Returns(new HostString("localhost"));
            MockHttpContext.Setup(r => r.Request).Returns(mockHttpRequest.Object);

            controllerContext.HttpContext = MockHttpContext.Object;
        }

        protected void SetUserIdentity(IEnumerable<Claim> claims, bool isAuthenticated = true)
        {
            var claimsIdentity = isAuthenticated ? new ClaimsIdentity(claims, "basic") : new ClaimsIdentity(claims);

            var claimsPrincipal = new ClaimsPrincipal(new List<ClaimsIdentity> { claimsIdentity });
            MockHttpContext.Setup(r => r.User).Returns(claimsPrincipal);
        }

        protected void SetupSatisfactionSurveyEntry(bool hasToMockAdd = false)
        {
            var satisfactionSurveyEntries = new List<SatisfactionSurveyEntry>
            {
                AutoFixture.Build<SatisfactionSurveyEntry>()
                    .With(x => x.Id , 1)
                    .With(x => x.comment, "TestComment")
                    .With(x => x.rating, "TestRating")
                    .With(x => x.url, "www.testurl.com")
                    .With(x => x.Date, DateTime.UtcNow)
                    .Create()
            };
            var satisfactionSurveyEntriesDbSet = satisfactionSurveyEntries.AsQueryable().BuildMockDbSet();
            MockLpDbContext.Setup(context => context.SatisfactionSurveyEntry).Returns(satisfactionSurveyEntriesDbSet.Object);
            MockLpDbContext.Setup(c => c.Set<SatisfactionSurveyEntry>()).Returns(satisfactionSurveyEntriesDbSet.Object);

            if (hasToMockAdd)
            {
                satisfactionSurveyEntriesDbSet
                    .Setup(_ => _.AddAsync(It.IsAny<SatisfactionSurveyEntry>(), It.IsAny<CancellationToken>()))
                    .Callback((SatisfactionSurveyEntry model, CancellationToken token) =>
                    {
                        model.Id = 10; satisfactionSurveyEntries.Add(model);
                    })
                    .Returns((SatisfactionSurveyEntry model, CancellationToken token) => ValueTask.FromResult((EntityEntry<SatisfactionSurveyEntry>)null!)!);
            }
        }
    }
}
