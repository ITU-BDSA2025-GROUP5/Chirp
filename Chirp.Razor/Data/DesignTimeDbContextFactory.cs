using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Chirp.Razor.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CheepDbContext>
{
    public CheepDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CheepDbContext>();
        optionsBuilder.UseSqlite("Data Source=cheep.db");
        return new CheepDbContext(optionsBuilder.Options);
    }
}