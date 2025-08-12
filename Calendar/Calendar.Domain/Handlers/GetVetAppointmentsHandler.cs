using AutoMapper;
using MediatR;
using Calendar.DataAccess.Repositories;
using Calendar.Domain.Models.Input;
using Calendar.Domain.Models.Output;

namespace Calendar.Domain.Handlers
{
    /// <summary>
    /// Handles retrieval of veterinarian appointments within a date range.
    /// </summary>
    public class GetVetAppointmentsHandler(
        IAppointmentsRepository appointmentsRepository,
        IAnimalsRepository animalsRepository,
        IMapper mapper) : IRequestHandler<GetVetAppointmentsInput, GetVetAppointmentsOutput>
    {
        private readonly IAppointmentsRepository _appointmentsRepository = appointmentsRepository;
        private readonly IAnimalsRepository _animalsRepository = animalsRepository;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Retrieves appointments for a veterinarian, including animal and owner details.
        /// </summary>
        public async Task<GetVetAppointmentsOutput> Handle(GetVetAppointmentsInput input, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentsRepository
                .GetByVeterinarianAndDateRangeAsync(
                    input.VeterinarianId,
                    input.StartDate,
                    input.EndDate,
                    cancellationToken);

            var animalIds = appointments.Select(a => a.AnimalId).Distinct().ToList();
            var animals = await _animalsRepository.GetAllAsync(cancellationToken);
            var animalsById = animals.Where(a => animalIds.Contains(a.Id)).ToDictionary(a => a.Id);

            var result = appointments
                .Select(appointment =>
                {
                    var vetAppointment = _mapper.Map<VetAppointment>(appointment);
                    if (animalsById.TryGetValue(appointment.AnimalId, out var animal))
                    {
                        vetAppointment.AnimalName = animal.Name;
                        vetAppointment.OwnerName = animal.OwnerName;
                    }
                    return vetAppointment;
                })
                .ToList();

            return new GetVetAppointmentsOutput { Appointments = result };
        }
    }
}
