using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Extensions;
using Contracts.Requests;
using Contracts.Responses;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController(IAnimalsRepository animalsRepository) : ControllerBase
{
    private readonly IAnimalsRepository _animalsRepository = animalsRepository;

    [HttpGet("{id}")]
    public async Task<ActionResult<AnimalResponse>> GetAnimal(Guid id, CancellationToken cancellationToken)
    {
        var animal = await _animalsRepository.GetByIdAsync(id, cancellationToken);
        if (animal == null)
        {
            return NotFound("Animal not found.");
        }

        return Ok(animal.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<AnimalResponse>> CreateAnimal([FromBody] AnimalRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return BadRequest("Animal request cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Animal name is required.");
        }

        var animal = request.ToEntity();

        await _animalsRepository.AddAsync(animal, cancellationToken);

        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal.ToResponse());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(Guid id, CancellationToken cancellationToken)
    {
        var animal = await _animalsRepository.GetByIdAsync(id, cancellationToken);
        if (animal == null)
        {
            return NotFound("Animal not found.");
        }

        await _animalsRepository.RemoveAsync(id, cancellationToken);
        return NoContent();
    }
}