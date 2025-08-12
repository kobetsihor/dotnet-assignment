namespace Calendar.Domain.Models.Output
{
    public class VetAppointment
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string AnimalName { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
