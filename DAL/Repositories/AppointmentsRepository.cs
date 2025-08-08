using DAL.Entities;
using DAL.Data;

namespace DAL.Repositories
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        public IEnumerable<Appointment> GetAll() => AppointmentData.Appointments;

        public Appointment? GetById(Guid id) => AppointmentData.Appointments.FirstOrDefault(a => a.Id == id);

        public void Add(Appointment appointment) => AppointmentData.Appointments.Add(appointment);
    }
}
