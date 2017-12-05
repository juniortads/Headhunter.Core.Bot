namespace Headhunter.Core.Bot.Infrastructure.Logging
{
    public class QueueLoggerSettings
    {
        public QueueLoggerSettings()
        {
            CompressMessage = false;
            OverflowHanding = LargeMessageMode.Discard;
            MessageTrimRate = 0.10f;
        }

        public bool CompressMessage { get; set; }
        public LargeMessageMode OverflowHanding { get; set; }
        public float MessageTrimRate;
    }
}
