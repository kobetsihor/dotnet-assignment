using DAL.Entities;
using DAL.Data;

namespace DAL.Repositories
{
    public class AnimalsRepository(AppDbContext context) : IAnimalsRepository
    {
        private readonly AppDbContext _context = context;

        public IEnumerable<Animal> GetAll() => _context.Animals.ToList();

        public Animal? GetById(Guid id) => _context.Animals.Find(id);

        public void Add(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var animal = _context.Animals.Find(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                _context.SaveChanges();
            }
        }
    }
}
