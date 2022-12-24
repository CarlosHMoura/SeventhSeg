using Microsoft.EntityFrameworkCore;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Infra.Data.Extensions;
using System.Diagnostics;

namespace SeventhSeg.Infra.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.DisableUnicode();
    }

    public DbSet<Server> Servers { get; set; }
    public DbSet<Movie> Movies { get; set; }
}
