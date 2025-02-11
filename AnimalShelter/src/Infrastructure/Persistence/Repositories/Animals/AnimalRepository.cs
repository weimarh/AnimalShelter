using Domain.Animals;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Animals;

public class AnimalRepository : IAnimalRepository
{
    private readonly ApplicationDbContext _context;

    public AnimalRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Animal animal) => 
        await _context.Animals.AddAsync(animal);

    public async Task<bool> ExistsAsync(AnimalId id) => 
        await _context.Animals.AnyAsync(a => a.Id == id);

    public async Task<IReadOnlyList<Animal>> GetAllAsync() =>
        await _context.Animals.ToListAsync();

    public async Task<Animal?> GetByIdAsync(AnimalId id) => 
        await _context.Animals.SingleOrDefaultAsync(a => a.Id == id);

    public void Remove(Animal animal) => 
        _context.Animals.Remove(animal);

    public void Update(Animal animal) =>
        _context.Animals.Update(animal);
}
