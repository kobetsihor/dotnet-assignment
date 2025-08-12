using Calendar.Contracts.Requests;
using Calendar.Contracts.Responses;
using Calendar.DataAccess.Entities;

namespace Calendar.Infrastructure.Extensions
{
    public static class AppointmentMappingExtensions
    {
        public static AppointmentResponse ToResponse(this Appointment appointment)
        {
            return new AppointmentResponse
            {
                Id = appointment.Id,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                AnimalId = appointment.AnimalId,
                CustomerId = appointment.CustomerId,
                VeterinarianId = appointment.VeterinarianId,
                Notes = appointment.Notes,
                Status = appointment.Status.ToString()
            };
        }

        public static Appointment ToEntity(this AppointmentRequest request)
        {
            return new Appointment
            {
                Id = Guid.NewGuid(),
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                AnimalId = request.AnimalId,
                CustomerId = request.CustomerId,
                VeterinarianId = request.VeterinarianId,
                Notes = request.Notes,
                Status = AppointmentStatus.Scheduled
            };
        }

        public static VetAppointmentResponse ToVetResponse(this Appointment appointment, Animal animal)
        {
            return new VetAppointmentResponse
            {
                Id = appointment.Id,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                AnimalName = animal.Name,
                OwnerName = animal.OwnerName,
                Status = appointment.Status.ToString()
            };
        }
    }
}