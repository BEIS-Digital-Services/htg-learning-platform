using Beis.LearningPlatform.Web.Filters;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Beis.LearningPlatform.Web.Tests.FilterTests
{
    public class ExceptionInterceptionFilterTests
    {
        [Test]
        public async Task ShouldReturnCompletedTask()
        {
            // Arrange
            var mockExceptionContext = new ExceptionContext(
                new ActionContext
                {
                    HttpContext = new DefaultHttpContext(),
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor()
                }, new Mock<List<IFilterMetadata>>().Object)
            {
                Exception = new Exception("exception")
            };

            // Act
            await new ExceptionInterceptionFilter().OnExceptionAsync(mockExceptionContext);

            // Assert
            var result = mockExceptionContext.Result as RedirectToRouteResult;
            Assert.NotNull(result);
            result.RouteName.Should().Be("ErrorGet");
        }
    }
}