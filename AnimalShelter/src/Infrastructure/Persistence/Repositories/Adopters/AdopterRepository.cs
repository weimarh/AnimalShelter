using Domain.Adopters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Adopters;

public class AdopterRepository : IAdopterRepository
{
    private readonly ApplicationDbContext _context;

    public AdopterRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Adopter animal) => 
        await _context.AddAsync(animal);

    public async Task<bool> ExistsAsync(AdopterId id) =>
        await _context.Adopters.AnyAsync(a => a.Id == id);

    public async Task<IReadOnlyList<Adopter>> GetAllAsync() =>
        await _context.Adopters.ToListAsync();

    public async Task<Adopter?> GetByIdAsync(AdopterId id) =>  
        await _context.Adopters.SingleOrDefaultAsync(a => a.Id == id);

    public void Remove(Adopter animal) =>
        _context.Adopters.Remove(animal);

    public void Update(Adopter animal) => 
        _context.Adopters.Update(animal);
}
