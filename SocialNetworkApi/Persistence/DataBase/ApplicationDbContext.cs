using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain;

namespace SocialNetwork.Persistence.DataBase;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Post> Post { get; set; }    
}
