using Calendar.Domain.Models.Output;
using MediatR;

namespace Calendar.Domain.Models.Input
{
    public class GetAppointmentInput : IRequest<GetAppointmentOutput>
    {
        public Guid Id { get; set; }
    }
}