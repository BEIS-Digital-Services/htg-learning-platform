namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface IEmailResponseHelper
    {
        Task<IEmailDto> ConvertToResultsEmail(DiagnosticToolForm form);

        FormTypes FormType { get; }
    }    
}