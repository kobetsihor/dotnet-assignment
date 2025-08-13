using AutoMapper;
using MediatR;
using Calendar.DataAccess.Repositories;
using Calendar.Domain.Models.Input;
using Calendar.Domain.Models.Output;

namespace Calendar.Domain.Handlers
{
    public class GetAnimalHandler(IAnimalsRepository animalsRepository, IMapper mapper) : IRequestHandler<GetAnimalInput, GetAnimalOutput>
    {
        private readonly IAnimalsRepository _animalsRepository = animalsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<GetAnimalOutput> Handle(GetAnimalInput input, CancellationToken cancellationToken)
        {
            var animal = await _animalsRepository.GetByIdAsync(input.Id, cancellationToken);
            return animal == null
                ? throw new KeyNotFoundException("Animal not found.")
                : _mapper.Map<GetAnimalOutput>(animal);
        }
    }
}