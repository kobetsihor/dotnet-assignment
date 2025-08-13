using Calendar.Contracts.Requests;
using FluentValidation;

namespace Calendar.Api.Validation
{
    public class CreateAnimalRequestValidator : AbstractValidator<CreateAnimalRequest>
    {
        public CreateAnimalRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithName(nameof(CreateAnimalRequest.Name))
                .WithMessage($"{nameof(CreateAnimalRequest.Name)} is required.");

            RuleFor(x => x.BirthDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithName(nameof(CreateAnimalRequest.BirthDate))
                .WithMessage($"{nameof(CreateAnimalRequest.BirthDate)} cannot be in the future.");

            RuleFor(x => x.OwnerId)
                .NotEmpty()
                .WithName(nameof(CreateAnimalRequest.OwnerId))
                .WithMessage($"{nameof(CreateAnimalRequest.OwnerId)} is required.");

            RuleFor(x => x.OwnerName)
                .NotEmpty()
                .WithName(nameof(CreateAnimalRequest.OwnerName))
                .WithMessage($"{nameof(CreateAnimalRequest.OwnerName)} is required.");

            RuleFor(x => x.OwnerEmail)
                .NotEmpty()
                .WithName(nameof(CreateAnimalRequest.OwnerEmail))
                .WithMessage($"{nameof(CreateAnimalRequest.OwnerEmail)} is required.")
                .EmailAddress()
                .WithMessage($"{nameof(CreateAnimalRequest.OwnerEmail)} must be a valid email address.");
        }
    }
}