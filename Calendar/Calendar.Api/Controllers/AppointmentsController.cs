using Calendar.Contracts.Requests;
using Calendar.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Calendar.Domain.Models.Input;

namespace Calendar.Api.Controllers
{
    /// <summary>
    /// Controller for managing appointment-related endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Gets a single appointment by its unique identifier.
        /// </summary>
        /// <param name="id">Appointment ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Appointment details or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAppointmentResponse>> GetAppointment(Guid id, CancellationToken cancellationToken)
        {
            var input = new GetAppointmentInput { Id = id };
            var appointment = await _mediator.Send(input, cancellationToken);
            if (appointment == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<GetAppointmentResponse>(appointment);
            return Ok(response);
        }

        /// <summary>
        /// Gets all appointments for a specific veterinarian within a date range.
        /// </summary>
        /// <param name="veterinarianId">Veterinarian ID.</param>
        /// <param name="request">Date range request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of vet appointments.</returns>
        [HttpGet("vet/{veterinarianId}")]
        public async Task<ActionResult<GetVetAppointmentsResponse>> GetVetAppointments(
            Guid veterinarianId,
            [FromQuery] GetVetAppointmentsRequest request,
            CancellationToken cancellationToken)
        {
            var input = new GetVetAppointmentsInput
            {
                VeterinarianId = veterinarianId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            var result = await _mediator.Send(input, cancellationToken);
            var response = _mapper.Map<GetVetAppointmentsResponse>(result);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="request">Appointment creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Created appointment details.</returns>
        [HttpPost]
        public async Task<ActionResult<GetAppointmentResponse>> CreateAppointment([FromBody] CreateAppointmentRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest("Appointment request cannot be null.");
            }

            if (request.AnimalId == Guid.Empty || request.CustomerId == Guid.Empty)
            {
                return BadRequest("AnimalId and CustomerId are required.");
            }

            var input = _mapper.Map<CreateAppointmentInput>(request);
            var appointment = await _mediator.Send(input, cancellationToken);
            var response = _mapper.Map<GetAppointmentResponse>(appointment);

            return CreatedAtAction(nameof(GetAppointment), new { id = response.Id }, response);
        }

        /// <summary>
        /// Updates the status of an appointment.
        /// </summary>
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<GetAppointmentResponse>> UpdateAppointmentStatus(
            Guid id,
            [FromBody] UpdateAppointmentStatusRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest("Invalid request.");

            var input = new UpdateAppointmentStatusInput
            {
                AppointmentId = id,
                Status = request.Status
            };
           
            var result = await _mediator.Send(input, cancellationToken);
            return Ok(result);
        }
    }
}