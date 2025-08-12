using AutoMapper;
using Calendar.DataAccess.Entities;
using Calendar.DataAccess.Repositories;
using Calendar.Domain.Models.Input;
using Calendar.Domain.Models.Output;
using MediatR;

namespace Calendar.Domain.Handlers
{
    public class CreateAnimalHandler(
        IAnimalsRepository animalsRepository,
        IMapper mapper) : IRequestHandler<CreateAnimalInput, CreateAnimalOutput>
    {
        private readonly IAnimalsRepository _animalsRepository = animalsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CreateAnimalOutput> Handle(CreateAnimalInput input, CancellationToken cancellationToken)
        {
            var animal = _mapper.Map<Animal>(input);
            await _animalsRepository.AddAsync(animal, cancellationToken);
            var output = _mapper.Map<CreateAnimalOutput>(animal);

            return output;
        }
    }
}
