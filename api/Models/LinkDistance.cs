namespace SchedulePath.Models
{
    public class LinkDistance : ActivityBase
    {
        public float FeedingBuffer { get; set; }
        public float PreviousFeedingBuffers { get; set; }
        public float TimePeriod { get; set; }
    }
}
