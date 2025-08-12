using Calendar.Contracts.Requests;
using Calendar.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Calendar.Domain.Models.Input;

namespace Calendar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet("{id}")]
        public async Task<ActionResult<CreateAnimalResponse>> GetAnimal(Guid id, CancellationToken cancellationToken)
        {
            var input = new GetAnimalInput { Id = id };
            var animal = await _mediator.Send(input, cancellationToken);
            if (animal == null)
            {
                return NotFound("Animal not found.");
            }

            var response = _mapper.Map<CreateAnimalResponse>(animal);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateAnimalResponse>> CreateAnimal([FromBody] CreateAnimalRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest("Animal request cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Animal name is required.");
            }

            var input = _mapper.Map<CreateAnimalInput>(request);
            var animal = await _mediator.Send(input, cancellationToken);
            var response = _mapper.Map<CreateAnimalResponse>(animal);

            return CreatedAtAction(nameof(GetAnimal), new { id = response.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(Guid id, CancellationToken cancellationToken)
        {
            var input = new DeleteAnimalInput { Id = id };
            await _mediator.Send(input, cancellationToken);
            return NoContent();
        }
    }
}