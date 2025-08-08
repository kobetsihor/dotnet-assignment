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
    public ActionResult<Appointment> GetAppointment(Guid id)
    {
        var appointment = _appointmentsRepository.GetById(id);
        if (appointment == null)
        {
            return NotFound();
        }
        return Ok(appointment);
    }

    [HttpPost]
    public ActionResult<Animal> CreateAppointment([FromBody] Appointment appointment)
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

        _appointmentsRepository.Add(appointment);

        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
    }
}