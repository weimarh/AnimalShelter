namespace Domain.Adopters;

public interface IAdopterRepository
{
    Task<IReadOnlyList<Adopter>> GetAllAsync();
    Task<Adopter?> GetByIdAsync(AdopterId id);
    Task<bool> ExistsAsync(AdopterId id);
    Task Add(Adopter animal);
    void Update(Adopter animal);
    void Remove(Adopter animal);
}
