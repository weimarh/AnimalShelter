namespace Domain.Animals;

public interface IAnimalRepository
{
    Task<IReadOnlyList<Animal>> GetAllAsync();
    Task<Animal?> GetByIdAsync(AnimalId id);
    Task<bool> ExistsAsync(AnimalId id);
    Task Add(Animal animal);
    void Update(Animal animal);
    void Remove(Animal animal);
}
