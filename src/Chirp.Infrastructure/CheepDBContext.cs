using Chirp.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Chirp.Infrastructure;

public class CheepDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Cheep> Cheeps { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;


    public CheepDbContext(DbContextOptions<CheepDbContext> options) : base(options)
    {}
    
}