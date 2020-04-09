using Microsoft.EntityFrameworkCore;

namespace ResourcehubApiDotNet.Models
{
    public class DatabaseContext : DbContext 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<OAuthTwitter> OAuthTwitter { get; set; }
    }
}