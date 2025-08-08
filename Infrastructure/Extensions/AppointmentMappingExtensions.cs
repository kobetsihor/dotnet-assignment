using Contracts.Requests;
using Contracts.Responses;
using DAL.Entities;

namespace Infrastructure.Extensions
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
    }
}