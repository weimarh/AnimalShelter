using Domain.Animals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Animals;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.ToTable("Animals");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            animalId => animalId.Id,
            value => new AnimalId(value));

        builder.Property(x => x.Name)?.HasMaxLength(50);

        builder.Property(x => x.Species).HasMaxLength(20).IsRequired();

        builder.Property(x => x.Breed)?.HasMaxLength(50);

        builder.Property(x => x.Sex).HasMaxLength(10).IsRequired();

        builder.Property(x => x.Color).HasMaxLength(50).IsRequired();
        
        builder.Property(x => x.Description)?.HasMaxLength(200);

        builder.Property(x => x.IntakeDate).IsRequired();

        builder.Property(x => x.AvailabilityStatus).HasMaxLength(20).IsRequired();

        builder.Property(x => x.MedicalHistory)?.HasMaxLength(200);

        builder.Property(x => x.SpecialNeeds)?.HasMaxLength(150);
        
    }
}
