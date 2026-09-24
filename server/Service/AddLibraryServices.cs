using System;
using Microsoft.Extensions.DependencyInjection;

namespace Service;

public static class DependencyInjection

{
    public static IServiceCollection AddLibraryServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<CityService>();

        return services;
    }
}
