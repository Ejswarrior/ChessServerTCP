using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ChessServerTCP.Models
{
    public class AppDBContext : IdentityDbContext
    {
        public IConfiguration _config { get; set; }

        public AppDBContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder modelBuilder)
        {
            modelBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));

        }

        public DbSet<User> User { get; set; }
    }
}
