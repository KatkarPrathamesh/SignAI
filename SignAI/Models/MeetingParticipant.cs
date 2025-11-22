namespace SignAI.Models
{
    public class MeetingParticipant
    {
        public long Id { get; set; }
        public long MeetingId { get; set; }
        public long UserId { get; set; }
        public string Role { get; set; }
        public DateTime? JoinedAt { get; set; }
        public DateTime? LeftAt { get; set; }
        public bool AudioEnabled { get; set; }
        public bool VideoEnabled { get; set; }
        public bool AiTranslationEnabled { get; set; }
    }
}
