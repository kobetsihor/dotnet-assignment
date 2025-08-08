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
    public ActionResult<Animal> GetAnimal(Guid id)
    {
        var animal = _animalsRepository.GetById(id);
        return Ok(animal);
    }

    [HttpPost]
    public ActionResult<Animal> CreateAnimal([FromBody] Animal animal)
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

        _animalsRepository.Add(animal);

        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(Guid id)
    {
        var animal = _animalsRepository.GetById(id);
        if (animal == null)
        {
            return NotFound("Animal not found.");
        }

        _animalsRepository.Remove(id);
        return NoContent();
    }
}