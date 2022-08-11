namespace Beis.LearningPlatform.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("error", Name = "ErrorGet")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}