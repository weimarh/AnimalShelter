using Application.Data;
using Domain.Adopters;
using Domain.Adoptions;
using Domain.Animals;
using Domain.Primitives;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Adopters;
using Infrastructure.Persistence.Repositories.Adoptions;
using Infrastructure.Persistence.Repositories.Animals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersitence(configuration);
        return services;
    }

    public static IServiceCollection AddPersitence(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Database")));

        services.AddScoped<IApplicationDbContext>(options =>
            options.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IUnitOfWork>(options =>
            options.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IAnimalRepository, AnimalRepository>();

        services.AddScoped<IAdopterRepository, AdopterRepository>();

        services.AddScoped<IAdoptionRepository, AdoptionRepository>();

        return services;
    }
}
