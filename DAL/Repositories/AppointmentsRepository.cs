using DAL.Entities;
using DAL.Data;

namespace DAL.Repositories
{
    public class AppointmentsRepository(AppDbContext context) : IAppointmentsRepository
    {
        private readonly AppDbContext _context = context;

        public IEnumerable<Appointment> GetAll() => _context.Appointments.ToList();

        public Appointment? GetById(Guid id) => _context.Appointments.Find(id);

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }
    }
}
