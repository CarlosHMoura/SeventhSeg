using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeventhSeg.Domain.Interfaces;
using SeventhSeg.Infra.Data.Context;
using SeventhSeg.Infra.Data.Repositories;

namespace SeventhSeg.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .EnableSensitiveDataLogging()
        );

        services.AddScoped<IServerRepository, ServerRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();

        return services;
    }
}
