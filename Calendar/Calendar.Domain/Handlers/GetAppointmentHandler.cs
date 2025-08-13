using AutoMapper;
using Calendar.DataAccess.Repositories;
using Calendar.Domain.Models.Input;
using Calendar.Domain.Models.Output;
using MediatR;

namespace Calendar.Domain.Handlers
{
    public class GetAppointmentHandler(
        IAppointmentsRepository appointmentsRepository,
        IMapper mapper) : IRequestHandler<GetAppointmentInput, GetAppointmentOutput>
    {
        private readonly IAppointmentsRepository _appointmentsRepository = appointmentsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<GetAppointmentOutput> Handle(GetAppointmentInput input, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentsRepository.GetByIdAsync(input.Id, cancellationToken);
            return appointment == null
                ? throw new KeyNotFoundException($"{nameof(appointment)} not found.")
                : _mapper.Map<GetAppointmentOutput>(appointment);
        }
    }
}