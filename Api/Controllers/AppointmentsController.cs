using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Extensions;
using Contracts.Requests;
using Contracts.Responses;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(IAppointmentsRepository appointmentsRepository) : ControllerBase
{
    private readonly IAppointmentsRepository _appointmentsRepository = appointmentsRepository;

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentResponse>> GetAppointment(Guid id, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentsRepository.GetByIdAsync(id, cancellationToken);
        if (appointment == null)
        {
            return NotFound();
        }

        return Ok(appointment.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentResponse>> CreateAppointment([FromBody] AppointmentRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return BadRequest("Appointment request cannot be null.");
        }

        if (request.AnimalId == Guid.Empty || request.CustomerId == Guid.Empty)
        {
            return BadRequest("AnimalId and CustomerId are required.");
        }

        var appointment = request.ToEntity();

        await _appointmentsRepository.AddAsync(appointment, cancellationToken);

        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment.ToResponse());
    }
}