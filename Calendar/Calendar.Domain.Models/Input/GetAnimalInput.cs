using Calendar.Domain.Models.Output;
using MediatR;

namespace Calendar.Domain.Models.Input
{
    public class GetAnimalInput : IRequest<GetAnimalOutput?>
    {
        public Guid Id { get; set; }
    }
}