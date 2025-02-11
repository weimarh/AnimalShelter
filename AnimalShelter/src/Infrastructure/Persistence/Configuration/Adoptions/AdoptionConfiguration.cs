using Domain.Adopters;
using Domain.Adoptions;
using Domain.Animals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Adoptions;

public class AdoptionConfiguration : IEntityTypeConfiguration<Adoption>
{
    public void Configure(EntityTypeBuilder<Adoption> builder)
    {
        builder.ToTable("Adoptions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            conversionId => conversionId.Id,
            value => new AdoptionId(value));
        
        builder.Property(x => x.AnimalId).HasConversion(
            conversionId => conversionId.Id,
            value => new AnimalId(value));
        
        builder.Property(x => x.AdopterId).HasConversion(
            conversionId => conversionId.Id,
            value => new AdopterId(value));
    }
}
