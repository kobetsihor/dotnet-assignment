using DAL.Entities;

namespace DAL.Repositories
{
    public interface IAppointmentsRepository
    {
        Task<List<Appointment>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default);
    }
}
