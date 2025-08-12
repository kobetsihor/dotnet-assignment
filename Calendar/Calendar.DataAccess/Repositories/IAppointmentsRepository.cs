using Calendar.DataAccess.Entities;

namespace Calendar.DataAccess.Repositories
{
    public interface IAppointmentsRepository
    {
        Task<List<Appointment>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default);

        Task<List<Appointment>> GetByVeterinarianAndDateRangeAsync(
            Guid veterinarianId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default);
    }
}
