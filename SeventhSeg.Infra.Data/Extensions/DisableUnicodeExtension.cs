using Microsoft.EntityFrameworkCore;

namespace SeventhSeg.Infra.Data.Extensions;

public static class DisableUnicodeExtension
{
    public static void DisableUnicode(this ModelBuilder modelBuilder)
    {
        var properties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(p => p.GetProperties())
            .Where(p => p.ClrType == typeof(string) && p.GetColumnType() == null);

        foreach (var property in properties)
        {
            property.SetIsUnicode(false);
        }
    }
}
