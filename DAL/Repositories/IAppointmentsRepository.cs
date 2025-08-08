using DAL.Entities;

namespace DAL.Repositories
{
    public interface IAppointmentsRepository
    {
        IEnumerable<Appointment> GetAll();

        Appointment? GetById(Guid id);

        void Add(Appointment appointment);
    }
}
