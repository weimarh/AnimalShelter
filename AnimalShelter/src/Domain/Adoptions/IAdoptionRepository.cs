namespace Domain.Adoptions;

public interface IAdoptionRepository
{
    Task<IReadOnlyList<Adoption>> GetAllAsync();
    Task<Adoption?> GetByIdAsync(AdoptionId id);
    Task<bool> ExistsAsync(AdoptionId id);
    Task Add(Adoption adoption);
    void Update(Adoption adoption);
    void Remove(Adoption adoption);
}
