namespace Calendar.Contracts.Requests
{
    public class CreateAppointmentRequest
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Guid AnimalId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid VeterinarianId { get; set; }

        public string? Notes { get; set; }
    }
}