using MediatR;

namespace Calendar.Domain.Models.Input
{
    public class DeleteAnimalInput : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}