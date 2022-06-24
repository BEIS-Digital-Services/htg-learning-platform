namespace Beis.LearningPlatform.Web.Services
{
    /// <summary>
    /// An interface that defines the behaviour of a Diagnostic Tool Form service.
    /// </summary>
    public interface IDiagnosticToolFormService
    {
        /// <summary>
        /// Loads a new, populated instance of the Diagnostic Tool Form.
        /// </summary>
        /// <returns>A DiagnosticToolForm that was loaded.</returns>
        DiagnosticToolForm LoadNewForm(FormTypes formTypes);
    }
}