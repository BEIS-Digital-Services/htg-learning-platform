namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface IEmailResponseHelperFactory
    {
        IEmailResponseHelper Get(FormTypes formType);
    }
}