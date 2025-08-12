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
    public class AppointmentsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

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
    }
}