using AutoMapper;
using MediatR;
using Calendar.DataAccess.Repositories;
using Calendar.Domain.Models.Input;
using Calendar.Domain.Models.Output;

namespace Calendar.Domain.Handlers
{
    public class GetAppointmentHandler(
        IAppointmentsRepository appointmentsRepository,
        IMapper mapper) : IRequestHandler<GetAppointmentInput, GetAppointmentOutput?>
    {
        private readonly IAppointmentsRepository _appointmentsRepository = appointmentsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<GetAppointmentOutput?> Handle(GetAppointmentInput input, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentsRepository.GetByIdAsync(input.Id, cancellationToken);
            return appointment == null ? null : _mapper.Map<GetAppointmentOutput>(appointment);
        }
    }
}