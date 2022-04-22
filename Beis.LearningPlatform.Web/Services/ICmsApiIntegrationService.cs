using System.Threading.Tasks;

namespace Beis.LearningPlatform.Web.Services
{
    /// <summary>
    /// An interface that defines the behaviour of a service that integrates with a CMS's API.
    /// </summary>
    public interface ICmsApiIntegrationService
    {
        /// <summary>
        /// Performs a GET from the CMS API.
        /// </summary>
        /// <param name="apiAction">A string containing the API action.</param>
        /// <returns>A Task representing the asynchronous operation.  A string containing the returned data.</returns>
        Task<string> Get(string apiAction);
    }
}