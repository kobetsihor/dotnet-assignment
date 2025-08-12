using MediatR;
using AutoMapper;
using Calendar.Domain.Models.Input;
using Calendar.Domain.Models.Output;
using Calendar.DataAccess.Entities;
using Calendar.DataAccess.Repositories;

namespace Calendar.Domain.Handlers
{
    public class CreateAppointmentHandler(IAppointmentsRepository appointmentsRepository, IMapper mapper) : IRequestHandler<CreateAppointmentInput, CreateAppointmentOutput>
    {
        private readonly IAppointmentsRepository _appointmentsRepository = appointmentsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CreateAppointmentOutput> Handle(CreateAppointmentInput input, CancellationToken cancellationToken)
        {
            var appointment = _mapper.Map<Appointment>(input);
            await _appointmentsRepository.AddAsync(appointment, cancellationToken);
            return _mapper.Map<CreateAppointmentOutput>(appointment);
        }
    }
}
