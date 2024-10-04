using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities;

namespace SocialNetwork.Persistence.DataBase;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Friend>()
            .HasKey(f => new { f.UserId, f.FriendId });

        modelBuilder.Entity<User>()
                .HasMany(u => u.LikedPosts)
                .WithMany(p => p.LikedByUsers)
                .UsingEntity(j => j.ToTable("UserLikes"));
        
        base.OnModelCreating(modelBuilder);
    }
}

