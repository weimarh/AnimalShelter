using Domain.Adopters;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Adopters;

public class AdopterConfiguration : IEntityTypeConfiguration<Adopter>
{
    public void Configure(EntityTypeBuilder<Adopter> builder)
    {
        builder.ToTable("Adopters");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            adopterId => adopterId.Id,
            value => new AdopterId(value));
        
        builder.Property(x => x.FirstName).HasMaxLength(10).IsRequired();

        builder.Property(x => x.LastName).HasMaxLength(10).IsRequired();

        builder.Property(x => x.PhoneNumber).HasConversion(
            phoneNumber => phoneNumber.Value,
            value => PhoneNumber.Create(value))
            .HasMaxLength(8);
        
        builder.OwnsOne(x => x.Address, addressBuilder => {
            addressBuilder.Property(x => x.Country).HasMaxLength(20).IsRequired();
            addressBuilder.Property(x => x.City).HasMaxLength(20).IsRequired();
            addressBuilder.Property(x => x.Street).HasMaxLength(30).IsRequired();
        });

        builder.Property(x => x.Email).HasMaxLength(20).IsRequired();
    }
}
