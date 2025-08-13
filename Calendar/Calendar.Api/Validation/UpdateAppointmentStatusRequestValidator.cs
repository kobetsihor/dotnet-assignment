using Calendar.Contracts.Requests;
using FluentValidation;
using Calendar.DataAccess.Enums;

namespace Calendar.Api.Validation
{
    public class UpdateAppointmentStatusRequestValidator : AbstractValidator<UpdateAppointmentStatusRequest>
    {
        public UpdateAppointmentStatusRequestValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithName(nameof(UpdateAppointmentStatusRequest.Status))
                .WithMessage($"{nameof(UpdateAppointmentStatusRequest.Status)} is required.")
                .Must(status => Enum.GetNames(typeof(AppointmentStatus)).Contains(status))
                .WithMessage($"{nameof(UpdateAppointmentStatusRequest.Status)} must be one of: {string.Join(", ", Enum.GetNames(typeof(AppointmentStatus)))}.");
        }
    }
}