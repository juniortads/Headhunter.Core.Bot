namespace Headhunter.Core.Bot.Models
{
    public class ActivityConversationLog
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ReplyToId { get; set; }
        public string Text { get; set; }
        public string LocalTimestamp { get; set; }
        public string Locale { get; set; }
        public string Type { get; set; }
        public string Timestamp { get; set; }
    }
}
