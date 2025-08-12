using MediatR;

namespace Calendar.Domain.Models.Input
{
    public class UpdateAppointmentStatusInput : IRequest<Unit>
    {
        public Guid AppointmentId { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
