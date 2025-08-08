using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Extensions;
using Contracts.Requests;
using Contracts.Responses;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(
    IAppointmentsRepository appointmentsRepository,
    IAnimalsRepository animalsRepository) : ControllerBase
{
    private readonly IAppointmentsRepository _appointmentsRepository = appointmentsRepository;
    private readonly IAnimalsRepository _animalsRepository = animalsRepository;

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

    [HttpGet("vet/{veterinarianId}")]
    public async Task<ActionResult<List<VetAppointmentResponse>>> GetVetAppointments(
        Guid veterinarianId,
        [FromQuery] VetAppointmentsQueryRequest query,
        CancellationToken cancellationToken)
    {
        var appointments = await _appointmentsRepository
            .GetByVeterinarianAndDateRangeAsync(
                veterinarianId,
                query.StartDate,
                query.EndDate,
                cancellationToken);

        var animalIds = appointments.Select(a => a.AnimalId).Distinct().ToList();
        var animals = await _animalsRepository.GetAllAsync(cancellationToken);
        var animalsById = animals.Where(a => animalIds.Contains(a.Id)).ToDictionary(a => a.Id);

        var result = appointments
            .Select(a => a.ToVetResponse(animalsById[a.AnimalId]))
            .ToList();

        return Ok(result);
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