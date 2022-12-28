using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SeventhSeg.Infra.Data.Context;

namespace SeventhSeg.API.Tests;

public class SeventhSegAPIApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

            services.AddSingleton<InMemoryDatabaseRoot>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("SeventhSegDb", root)
                .EnableServiceProviderCaching(false)
                .ConfigureWarnings(x => x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning))) ;
        });

        return base.CreateHost(builder);
    }
}
