using DAL.Entities;

namespace DAL.Repositories
{
    public interface IAnimalsRepository
    {
        IEnumerable<Animal> GetAll();

        Animal? GetById(Guid id);

        void Add(Animal animal);

        void Remove(Guid id);
    }
}
