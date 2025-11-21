namespace EphemeralRealTimeChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Text { get; set; }
        public DateTime SentAtUtc { get; set; }
    }
}
