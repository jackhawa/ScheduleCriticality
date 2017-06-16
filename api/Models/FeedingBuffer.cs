namespace SchedulePath.Models
{
    public class FeedingBuffer : ActivityBase
    {
        public float PreviousFeedingBuffers { get; set; }
        public float Buffer { get; set; }
        public float TimePeriod { get; set; }
        public float ControllingLinkShift { get; set; }
    }
}
