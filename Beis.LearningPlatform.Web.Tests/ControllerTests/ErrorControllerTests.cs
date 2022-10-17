using System.Diagnostics;

namespace Beis.LearningPlatform.Web.Tests.ControllerTests;

public class ErrorControllerTests
{
    private Activity _activity;
    private readonly ErrorController _controller = new ();

    [SetUp]
    protected void Setup()
    {
        _activity = new Activity("UnitTest").Start();
    }

    [TearDown]
    protected void Teardown()
    {
        _activity.Stop();
    }
    
    [Test]
    public void Should_return_view_with_valid_model()
    {
        var result = _controller.Error();
        
        Assert.NotNull(result);
        Assert.That(result, Is.TypeOf<ViewResult>());
    }
}