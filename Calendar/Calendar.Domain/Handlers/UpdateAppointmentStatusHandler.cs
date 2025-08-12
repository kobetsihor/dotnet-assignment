using Calendar.DataAccess.Enums;
using Calendar.DataAccess.Repositories;
using Calendar.Domain.Models.Input;
using MediatR;

namespace Calendar.Domain.Handlers
{
    /// <summary>
    /// Handles updating the status of an appointment, including business rules and notifications.
    /// </summary>
    public class UpdateAppointmentStatusHandler(
        IAppointmentsRepository appointmentsRepository,
        IAnimalsRepository animalsRepository
    ) : IRequestHandler<UpdateAppointmentStatusInput, Unit>
    {
        private readonly IAppointmentsRepository _appointmentsRepository = appointmentsRepository;
        private readonly IAnimalsRepository _animalsRepository = animalsRepository;
        private static readonly AppointmentStatus[] ValidStatuses =
            [
                AppointmentStatus.Scheduled,
                AppointmentStatus.Completed,
                AppointmentStatus.Cancelled
            ];

        /// <summary>
        /// Updates the status of an appointment, enforcing cancellation rules and sending notifications.
        /// </summary>
        /// <param name="input">Input containing appointment ID and new status.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Unit result.</returns>
        public async Task<Unit> Handle(UpdateAppointmentStatusInput input, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentsRepository.GetByIdAsync(input.AppointmentId, cancellationToken)
                ?? throw new KeyNotFoundException("Appointment not found.");

            if (!Enum.TryParse<AppointmentStatus>(input.Status, true, out var newStatus) ||
                !ValidStatuses.Contains(newStatus))
                throw new ArgumentException("Invalid status value.");

            if (newStatus == AppointmentStatus.Cancelled && appointment.StartTime <= DateTime.UtcNow.AddHours(1))
                throw new InvalidOperationException("Cannot cancel within 1 hour of start time.");

            appointment.Status = newStatus;
            await _appointmentsRepository.UpdateAsync(appointment, cancellationToken);

            if (newStatus == AppointmentStatus.Cancelled)
            {
                var animal = await _animalsRepository.GetByIdAsync(appointment.AnimalId, cancellationToken)
                    ?? throw new KeyNotFoundException("Animal not found.");

                Console.WriteLine($"Email sent to {animal.OwnerEmail}");
            }

            return Unit.Value;
        }
    }
}
