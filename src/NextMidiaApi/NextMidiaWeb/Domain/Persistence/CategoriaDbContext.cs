using NextMidiaWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NextMidiaWeb.Domain.Persistence
{
    public class CategoriaDbContext : DbContext
    {
        public CategoriaDbContext(DbContextOptions<CategoriaDbContext> options) : base(options)
        {

        }

        public DbSet<Categoria> categoria { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Categoria>(entity =>
            {
                entity.HasKey(categoria => categoria.Id);

                entity.Property(categoria => categoria.Nome).IsRequired();
            });
        }
    }
}
