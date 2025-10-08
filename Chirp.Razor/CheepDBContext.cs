using Microsoft.EntityFrameworkCore;
namespace Chirp.Razor;


public class CheepDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;


    public CheepDbContext(DbContextOptions<CheepDbContext> options) : base(options)
    {
        
    }
    
}