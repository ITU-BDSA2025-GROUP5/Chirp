using Chirp.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Chirp.Infrastructure;

public class CheepDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Cheep> Cheeps { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;

    public DbSet<Follow> Follows { get; set; } = default!;


    public CheepDbContext(DbContextOptions<CheepDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<User>()
         .HasOne(u => u.ApplicationUser)
         .WithOne(a => a.DomainUser!)
         .HasForeignKey<User>(u => u.ApplicationUserId)
         .OnDelete(DeleteBehavior.Cascade);

        b.Entity<User>()
         .HasIndex(u => u.ApplicationUserId)
         .IsUnique();
        b.Entity<Follow>(entity =>
        {
    
            entity.HasKey(f => new { f.FollowerId, f.FolloweeId });
            
            entity.HasOne(f => f.Follower)
                .WithMany()  // no navigation on User
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(f => f.Followee)
                .WithMany()  // no navigation on User
                .HasForeignKey(f => f.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasIndex(f => f.FollowerId);
            entity.HasIndex(f => f.FolloweeId);

            entity.ToTable("Follows");
        });
    }

public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
{
    AutoCreateDomainUsers();
    return await base.SaveChangesAsync(ct);
}

public override int SaveChanges()
{
    AutoCreateDomainUsers();
    return base.SaveChanges();
}

private void AutoCreateDomainUsers()
{
    // Find newly added Identity users in this save
    var newAppUsers = ChangeTracker.Entries<ApplicationUser>()
        .Where(e => e.State == EntityState.Added)
        .Select(e => e.Entity)
        .ToList();

    if (newAppUsers.Count == 0) return;

    // Avoid double-adding within the same context
    var alreadyPlanned = ChangeTracker.Entries<User>()
        .Where(e => e.State == EntityState.Added)
        .Select(e => e.Entity.ApplicationUserId)
        .ToHashSet();

    foreach (var au in newAppUsers)
    {
        if (alreadyPlanned.Contains(au.Id)) continue;

        // Create the domain user row
        Users.Add(new User
        {
            ApplicationUserId = au.Id,               // FK to Identity
            Name  = au.UserName ?? au.Email ?? "user",
            Email = au.Email ?? string.Empty,
            Cheeps = new List<Cheep>()
        });
    }
}

}