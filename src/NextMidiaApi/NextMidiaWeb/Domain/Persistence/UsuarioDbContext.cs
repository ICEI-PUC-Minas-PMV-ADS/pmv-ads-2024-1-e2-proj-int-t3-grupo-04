using NextMidiaWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NextMidiaWeb.Domain.Persistence
{
    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>(entity =>
            {
                entity.HasKey(usuario => usuario.Id);

                entity.Property(usuario => usuario.Nome).IsRequired();
            });
        }



    }
}
