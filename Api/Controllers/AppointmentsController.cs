using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(IAppointmentsRepository appointmentsRepository) : ControllerBase
{
    private readonly IAppointmentsRepository _appointmentsRepository = appointmentsRepository;

    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(Guid id, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentsRepository.GetByIdAsync(id, cancellationToken);
        if (appointment == null)
        {
            return NotFound();
        }
        return Ok(appointment);
    }

    [HttpPost]
    public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] Appointment appointment, CancellationToken cancellationToken)
    {
        if (appointment == null)
        {
            return BadRequest("Appointment cannot be null.");
        }

        if (appointment.AnimalId == Guid.Empty || appointment.CustomerId == Guid.Empty)
        {
            return BadRequest("AnimalId and CustomerId are required.");
        }

        appointment.Id = Guid.NewGuid();

        await _appointmentsRepository.AddAsync(appointment, cancellationToken);

        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
    }
}