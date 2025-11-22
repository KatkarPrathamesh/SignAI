namespace SignAI.Models
{
    public class Meeting
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long HostId { get; set; }
        public DateTime ScheduledTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Status { get; set; }
    }

}
