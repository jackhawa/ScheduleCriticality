using SchedulePath.Models;

namespace SchedulePath.Models
{
    public enum ActivityDirection
    {
        Normal,
        Reverse
    }

    public class ActivityWithDirection
    {
        public ActivityWithDirection()
        {
            Flip = 1;
        }
        public Activity Activity { get; set; }
        public ActivityDirection Direction { get; set; }
        public LinkDistance LinkDistance { get; set; }
        public FeedingBuffer FeedingBuffer { get; set; }
        public float LinkShift { get; set; }
        public float Flip { get; set; }
    }
}
