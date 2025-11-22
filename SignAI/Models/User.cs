namespace SignAI.Models
{
    public class User
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public bool EmailValidationStatus { get; set; }
        public bool MobileValidationStatus { get; set; }
        public bool IsLocked { get; set; }
        public bool IsMultiSessionAllowed { get; set; }
    }

}
