using NextMidiaWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NextMidiaWeb.Domain.Persistence
{
    public class ComentarioDbContext : DbContext
    {
        public ComentarioDbContext(DbContextOptions<ComentarioDbContext> options) : base(options)
        {

        }

        public DbSet<Comentario> comentario { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Comentario>(entity =>
            {
                entity.HasKey(comentario => comentario.Id);

                entity.Property(comentario => comentario.Texto).IsRequired();
                entity.Property(comentario => comentario.Nota).IsRequired();
            });
        }



    }
}
