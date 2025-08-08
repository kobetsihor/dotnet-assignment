using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController(IAnimalsRepository animalsRepository) : ControllerBase
{
    private readonly IAnimalsRepository _animalsRepository = animalsRepository;

    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(Guid id, CancellationToken cancellationToken)
    {
        var animal = await _animalsRepository.GetByIdAsync(id, cancellationToken);
        return Ok(animal);
    }

    [HttpPost]
    public async Task<ActionResult<Animal>> CreateAnimal([FromBody] Animal animal, CancellationToken cancellationToken)
    {
        if (animal == null)
        {
            return BadRequest("Animal cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(animal.Name))
        {
            return BadRequest("Animal name is required.");
        }

        animal.Id = Guid.NewGuid();

        await _animalsRepository.AddAsync(animal, cancellationToken);

        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
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