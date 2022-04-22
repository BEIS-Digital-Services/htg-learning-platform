namespace Beis.LearningPlatform.Web.Services
{
    public interface IHtmlTextService
    {        
        string ReplaceLineBreaks(string input, string replacement = "<br />");
        string CleanWhiteSpace(string input, string replacement = " ");
    }
}