using DAL.Entities;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AnimalsRepository(AppDbContext context) : IAnimalsRepository
    {
        private readonly AppDbContext _context = context;

        public Task<List<Animal>> GetAllAsync(CancellationToken cancellationToken = default)
            => _context.Animals.ToListAsync(cancellationToken);

        public Task<Animal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => _context.Animals.FindAsync([id], cancellationToken).AsTask();

        public async Task AddAsync(Animal animal, CancellationToken cancellationToken = default)
        {
            await _context.Animals.AddAsync(animal, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var animal = await _context.Animals.FindAsync([id], cancellationToken);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
