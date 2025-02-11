using Domain.Adoptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Adoptions;

public class AdoptionRepository : IAdoptionRepository
{
    private readonly ApplicationDbContext _context;

    public AdoptionRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Adoption adoption) =>
        await _context.Adoptions.AddAsync(adoption);

    public async Task<bool> ExistsAsync(AdoptionId id) =>
        await _context.Adoptions.AnyAsync(a => a.Id == id);

    public async Task<IReadOnlyList<Adoption>> GetAllAsync() =>
        await _context.Adoptions.ToListAsync();

    public async Task<Adoption?> GetByIdAsync(AdoptionId id) =>
        await _context.Adoptions.SingleOrDefaultAsync(a => a.Id == id);

    public void Remove(Adoption adoption) =>
        _context.Adoptions.Remove(adoption);

    public void Update(Adoption adoption) =>
        _context.Adoptions.Update(adoption);
}
