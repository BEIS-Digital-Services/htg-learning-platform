namespace Beis.LearningPlatform.Library
{
    public class ExpressionOfInterestDto
    {
	    public int Id { get; set; }
	    public string PageName { get; set; }
	    public string UserName { get; set; }
	    public string UserEmail { get; set; }
	    public string UserBusinessName { get; set; }
	    public string UserPhone { get; set; }
	    public bool OptInMarketing { get; set; }
	    public bool OptInReadPrivacy { get; set; }
	    public DateTime RecordCreatedUtc { get; set; }
    }
}