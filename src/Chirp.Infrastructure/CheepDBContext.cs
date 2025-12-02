using Chirp.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public class CheepDbContext : IdentityDbContext<User>
{
    public DbSet<Cheep> Cheeps { get; set; } = default!;
    public new DbSet<User> Users { get; set; } = default!;
    public DbSet<Follow> Follows { get; set; } = default!;

    public CheepDbContext(DbContextOptions<CheepDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // Configure the relationship between Cheep and User
        b.Entity<Cheep>()
         .HasOne(c => c.User)
         .WithMany(u => u.Cheeps)
         .HasForeignKey(c => c.UserId)
         .OnDelete(DeleteBehavior.Cascade);

        // Configure the Follow entity
        b.Entity<Follow>(entity =>
        {
            entity.HasKey(f => new { f.FollowerId, f.FolloweeId });

            entity.HasOne(f => f.Follower)
                .WithMany() // No navigation property on User
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.Followee)
                .WithMany() // No navigation property on User
                .HasForeignKey(f => f.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(f => f.FollowerId);
            entity.HasIndex(f => f.FolloweeId);

            entity.ToTable("Follows");
        });
    }
}