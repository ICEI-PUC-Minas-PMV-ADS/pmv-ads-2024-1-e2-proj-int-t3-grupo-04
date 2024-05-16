using NextMidiaWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NextMidiaWeb.Domain.Persistence
{
    public class MidiaDbContext : DbContext
    {
        public MidiaDbContext(DbContextOptions<MidiaDbContext> options) : base(options)
        {

        }

        public DbSet<Midia> midias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Midia>(entity =>
            {
                entity.HasKey(midia => midia.Id);                
                entity.Property(midia => midia.Nome).IsRequired();                
            });
        }



    }
}
