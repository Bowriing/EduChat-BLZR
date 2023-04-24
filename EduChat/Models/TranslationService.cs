namespace EduChat.Models
{
    public class TranslationService
    {
        public int Id { get; set; }
        public string Languages { get; set; }
        public string Availability { get; set; }
        public string Rates { get; set; }

        public TimeSpan AvailabilityFrom { get; set; }
        public TimeSpan AvailabilityTo { get; set; }
    }

}
