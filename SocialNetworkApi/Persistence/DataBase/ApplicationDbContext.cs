using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain;

namespace SocialNetwork.Persistence.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Friend> Friends { get; set; } 
        public DbSet<Comment> Comments { get; set; }

    }
}



