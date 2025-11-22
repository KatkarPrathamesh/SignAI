namespace SignAI.DTOs
{
    public class CreateMeetingRequest
    {
        public string Title { get; set; }
        public long UserId { get; set; }
        public DateTime ScheduledTime { get; set; }
        public int DurationMinutes { get; set; }
    }

}
