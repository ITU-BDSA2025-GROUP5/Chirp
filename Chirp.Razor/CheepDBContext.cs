using Microsoft.EntityFrameworkCore;
namespace Chirp.Razor;


public class CheepDbContext : DbContext
{
    public DbSet<Cheep> Cheeps { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;


    public CheepDbContext(DbContextOptions<CheepDbContext> options) : base(options)
    {}
    
}