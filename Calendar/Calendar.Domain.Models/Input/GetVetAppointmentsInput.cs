using Calendar.Domain.Models.Output;
using MediatR;

namespace Calendar.Domain.Models.Input
{
    public class GetVetAppointmentsInput : IRequest<GetVetAppointmentsOutput>
    {
        public Guid VeterinarianId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}