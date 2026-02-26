using GHL.Application.Interfaces;
using GHL.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GHL.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IWorkSessionService, WorkSessionService>();
        
        return services;
    }
}
