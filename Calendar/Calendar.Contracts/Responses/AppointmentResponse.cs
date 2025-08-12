namespace Calendar.Contracts.Responses
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Guid AnimalId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid VeterinarianId { get; set; }

        public string? Notes { get; set; }

        public string Status { get; set; }
    }
}