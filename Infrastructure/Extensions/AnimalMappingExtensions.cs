using Contracts.Requests;
using Contracts.Responses;
using DAL.Entities;

namespace Infrastructure.Extensions
{
    public static class AnimalMappingExtensions
    {
        public static AnimalResponse ToResponse(this Animal animal)
        {
            return new AnimalResponse
            {
                Id = animal.Id,
                Name = animal.Name,
                BirthDate = animal.BirthDate,
                OwnerId = animal.OwnerId,
                OwnerName = animal.OwnerName,
                OwnerEmail = animal.OwnerEmail
            };
        }

        public static Animal ToEntity(this AnimalRequest request)
        {
            return new Animal
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                BirthDate = request.BirthDate,
                OwnerId = request.OwnerId,
                OwnerName = request.OwnerName,
                OwnerEmail = request.OwnerEmail
            };
        }
    }
}