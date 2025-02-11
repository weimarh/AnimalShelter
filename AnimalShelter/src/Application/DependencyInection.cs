using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Behaviors;

namespace Application;

public static class DependencyInection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>());
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly);

        return services;
    }
}
