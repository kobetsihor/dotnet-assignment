using Calendar.Contracts.Requests;
using FluentValidation;

namespace Calendar.Api.Validation
{
    public class GetVetAppointmentsRequestValidator : AbstractValidator<GetVetAppointmentsRequest>
    {
        public GetVetAppointmentsRequestValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEqual(default(DateTime))
                .WithName(nameof(GetVetAppointmentsRequest.StartDate))
                .WithMessage($"{nameof(GetVetAppointmentsRequest.StartDate)} is required.")
                .LessThanOrEqualTo(x => x.EndDate)
                .WithMessage($"{nameof(GetVetAppointmentsRequest.StartDate)} must be before or equal to {nameof(GetVetAppointmentsRequest.EndDate)}.");

            RuleFor(x => x.EndDate)
                .NotEqual(default(DateTime))
                .WithName(nameof(GetVetAppointmentsRequest.EndDate))
                .WithMessage($"{nameof(GetVetAppointmentsRequest.EndDate)} is required.")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage($"{nameof(GetVetAppointmentsRequest.EndDate)} must be after or equal to {nameof(GetVetAppointmentsRequest.StartDate)}.");
        }
    }
}