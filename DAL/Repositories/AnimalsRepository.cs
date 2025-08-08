using DAL.Entities;
using DAL.Data;

namespace DAL.Repositories
{
    public class AnimalsRepository : IAnimalsRepository
    {
        public IEnumerable<Animal> GetAll() => AnimalData.Animals;

        public Animal? GetById(Guid id) => AnimalData.Animals.FirstOrDefault(a => a.Id == id);

        public void Add(Animal animal) => AnimalData.Animals.Add(animal);

        public void Remove(Guid id)
        {
            var animal = GetById(id);
            if (animal != null)
            {
                AnimalData.Animals.Remove(animal);
            }
        }
    }
}
