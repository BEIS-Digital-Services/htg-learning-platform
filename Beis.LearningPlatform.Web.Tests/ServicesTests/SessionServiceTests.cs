using Microsoft.ApplicationInsights.Extensibility.Implementation;
using OpenQA.Selenium.DevTools.V95.CSS;

namespace Beis.LearningPlatform.Web.Tests.ServicesTests;

public class SessionServiceTests
{
    private SessionService _sessionService;
    private HttpContext _httpContext;
    private Mock<IHttpContextAccessor> _httpContextAccessor;

    [SetUp]
    public void Setup()
    {
        _sessionService = new SessionService();
        _httpContext = new DefaultHttpContext();
        _httpContext.Session = new MockHttpSession();
        _httpContextAccessor = new Mock<IHttpContextAccessor>();
        _httpContextAccessor.Setup(x => x.HttpContext).Returns(_httpContext);
    } 
    
    [TestCase(1, typeof(int))]
    [TestCase("string", typeof(string))]
    [TestCase(1.11, typeof(double))]
    [TestCase('c', typeof(char))]
    [Test]
    public void Should_save_simple_types_to_session(object value, Type t)
    {
        _sessionService.Set("value", value, _httpContext);
        _httpContext.Session.Get("value").Should().NotBeNull();
    }
    
    [Test]
    public void Should_save_object_types_to_session()
    {
        List<int> list = new List<int>()
        {
            1,2,3,4
        };
        _sessionService.Set("value", list, _httpContext);
        _httpContext.Session.Get("value").Should().NotBeNull();
    }

    [Test]
    public void Should_retrieve_previously_set_value()
    {
        _sessionService.Set("value", 1, _httpContext);
        _sessionService.TryGet("value", _httpContext, out int result);
        result.Should().Be(1);
    }

    [Test]
    public void Should_remove_value_from_session_data()
    {
        _sessionService.Set("value", 1, _httpContext);
        _sessionService.Remove("value", _httpContext);
        Assert.Throws<KeyNotFoundException>(() => _httpContext.Session.Get("value"));
    }
    

    private class MockHttpSession : ISession
    {
        readonly Dictionary<string, object> _sessionStorage = new Dictionary<string, object>();
        string ISession.Id => throw new NotImplementedException();
        bool ISession.IsAvailable => throw new NotImplementedException();
        IEnumerable<string> ISession.Keys => _sessionStorage.Keys;
        void ISession.Clear()
        {
            _sessionStorage.Clear();
        }
        Task ISession.CommitAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        Task ISession.LoadAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        void ISession.Remove(string key)
        {
            _sessionStorage.Remove(key);
        }
        void ISession.Set(string key, byte[] value)
        {
            _sessionStorage[key] = Encoding.UTF8.GetString(value);
        }
        bool ISession.TryGetValue(string key, out byte[] value)
        {
            if (_sessionStorage[key] != null)
            {
                value = Encoding.ASCII.GetBytes(_sessionStorage[key].ToString());
                return true;
            }
            value = null;
            return false;
        }
    }
    
    
}