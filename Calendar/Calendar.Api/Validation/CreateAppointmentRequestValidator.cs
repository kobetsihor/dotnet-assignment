using Calendar.Contracts.Requests;
using FluentValidation;

namespace Calendar.Api.Validation
{
    public class CreateAppointmentRequestValidator : AbstractValidator<CreateAppointmentRequest>
    {
        public CreateAppointmentRequestValidator()
        {
            RuleFor(x => x.AnimalId)
                .NotEmpty().WithName(nameof(CreateAppointmentRequest.AnimalId))
                .WithMessage($"{nameof(CreateAppointmentRequest.AnimalId)} is required.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithName(nameof(CreateAppointmentRequest.CustomerId))
                .WithMessage($"{nameof(CreateAppointmentRequest.CustomerId)} is required.");

            RuleFor(x => x.VeterinarianId)
                .NotEmpty().WithName(nameof(CreateAppointmentRequest.VeterinarianId))
                .WithMessage($"{nameof(CreateAppointmentRequest.VeterinarianId)} is required.");

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .WithName(nameof(CreateAppointmentRequest.StartTime))
                .WithMessage($"{nameof(CreateAppointmentRequest.StartTime)} must be before {nameof(CreateAppointmentRequest.EndTime)}.");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithName(nameof(CreateAppointmentRequest.EndTime))
                .WithMessage($"{nameof(CreateAppointmentRequest.EndTime)} must be after {nameof(CreateAppointmentRequest.StartTime)}.");
        }
    }
}