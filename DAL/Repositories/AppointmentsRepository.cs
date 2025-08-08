using DAL.Entities;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AppointmentsRepository(AppDbContext context) : IAppointmentsRepository
    {
        private readonly AppDbContext _context = context;

        public Task<List<Appointment>> GetAllAsync(CancellationToken cancellationToken = default)
            => _context.Appointments.ToListAsync(cancellationToken);

        public Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => _context.Appointments.FindAsync([id], cancellationToken).AsTask();

        public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default)
        {
            await _context.Appointments.AddAsync(appointment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<List<Appointment>> GetByVeterinarianAndDateRangeAsync(
            Guid veterinarianId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default)
                => _context.Appointments
                      .Where(a => a.VeterinarianId == veterinarianId
                                  && a.StartTime >= startDate
                                  && a.EndTime <= endDate)
                      .ToListAsync(cancellationToken);
    }
}
