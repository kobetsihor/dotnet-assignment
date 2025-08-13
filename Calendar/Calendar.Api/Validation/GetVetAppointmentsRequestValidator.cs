using Calendar.Contracts.Requests;
using FluentValidation;

namespace Calendar.Api.Validation
{
    public class GetVetAppointmentsRequestValidator : AbstractValidator<GetVetAppointmentsRequest>
    {
        public GetVetAppointmentsRequestValidator()
        {
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .WithName(nameof(GetVetAppointmentsRequest.StartDate))
                .WithMessage($"{nameof(GetVetAppointmentsRequest.StartDate)} must be before or equal to {nameof(GetVetAppointmentsRequest.EndDate)}.");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithName(nameof(GetVetAppointmentsRequest.EndDate))
                .WithMessage($"{nameof(GetVetAppointmentsRequest.EndDate)} must be after or equal to {nameof(GetVetAppointmentsRequest.StartDate)}.");
        }
    }
}