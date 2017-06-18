namespace SchedulePath.Models
{
    public class LinkWithActivity: Link
    {
        public Activity UpwardAct { get; set; }
        public Activity DownwardAct { get; set; }
    }
}
