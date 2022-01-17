using Microsoft.EntityFrameworkCore;
using Trabalho.Dominio;

namespace Trabalho.Infrastructure
{
    public class TrabalhoDbContext : DbContext
    {
        public TrabalhoDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<SessionMovie> Session { get; set; }
        public DbSet<Ticket> Ticket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
