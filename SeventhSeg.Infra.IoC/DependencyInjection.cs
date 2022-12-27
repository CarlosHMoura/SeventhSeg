using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeventhSeg.Application.Interfaces;
using SeventhSeg.Application.Mappings;
using SeventhSeg.Application.Services;
using SeventhSeg.Domain.Interfaces;
using SeventhSeg.Infra.Data.Context;
using SeventhSeg.Infra.Data.Repositories;

namespace SeventhSeg.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
       IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .EnableSensitiveDataLogging()
        );

        services.AddScoped<IServerRepository, ServerRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IRecyclerRepository, RecyclerRepository>();

        services.AddScoped<IServerService, ServerService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IRecyclerService, RecyclerService>();

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        return services;
    }
}
