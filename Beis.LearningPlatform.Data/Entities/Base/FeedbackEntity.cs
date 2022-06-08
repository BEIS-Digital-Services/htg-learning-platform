namespace Beis.LearningPlatform.Data.Entities.Base
{
    public class FeedbackEntity
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public DateTime Date { get; set; }
        public string url { get; set; }
    }
}