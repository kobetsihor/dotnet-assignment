using MediatR;
using Calendar.DataAccess.Repositories;
using Calendar.Domain.Models.Input;

namespace Calendar.Domain.Handlers
{
    public class DeleteAnimalHandler(IAnimalsRepository animalsRepository) : IRequestHandler<DeleteAnimalInput, Unit>
    {
        private readonly IAnimalsRepository _animalsRepository = animalsRepository;

        public async Task<Unit> Handle(DeleteAnimalInput input, CancellationToken cancellationToken)
        {
            _ = await _animalsRepository.GetByIdAsync(input.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Animal with id '{input.Id}' not found.");

            await _animalsRepository.RemoveAsync(input.Id, cancellationToken);
            return Unit.Value;
        }
    }
}