using Domain.Adopters;
using Domain.Adoptions;
using Domain.Animals;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Animal> Animals { get; set; }
    DbSet<Adopter> Adopters { get; set; }
    DbSet<Adoption> Adoptions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
