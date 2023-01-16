using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shoppy.Application.Behaviors;

namespace Shoppy.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddShoppyServices(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddMediatR(ConfigureMediatR, assemblies);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SaveChangesBehavior<,>));
        return services;
    }

    private static void ConfigureMediatR(MediatRServiceConfiguration configuration)
    {
        
    }
}