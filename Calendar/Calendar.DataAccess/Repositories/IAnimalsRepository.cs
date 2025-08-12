using Calendar.DataAccess.Entities;

namespace Calendar.DataAccess.Repositories
{
    public interface IAnimalsRepository
    {
        Task<List<Animal>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Animal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Animal animal, CancellationToken cancellationToken = default);

        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
