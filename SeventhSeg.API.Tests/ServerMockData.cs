using Microsoft.Extensions.DependencyInjection;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventhSeg.API.Tests;

public class ServerMockData
{
    public static async Task CreateServers(SeventhSegAPIApplication application, bool create)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var applicationDbContext = provider.GetRequiredService<ApplicationDbContext>())
            {
                await applicationDbContext.Database.EnsureCreatedAsync();

                if (create)
                {
                    await applicationDbContext.Servers.AddAsync(new Server("Local", "192.168.1.1", 80));

                    await applicationDbContext.SaveChangesAsync();
                }
            }
        }
    }
}
