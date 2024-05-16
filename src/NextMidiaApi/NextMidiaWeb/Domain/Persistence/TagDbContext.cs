using NextMidiaWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NextMidiaWeb.Domain.Persistence
{
    public class TagDbContext : DbContext
    {
        public TagDbContext(DbContextOptions<TagDbContext> options) : base(options)
        {

        }

        public DbSet<Tag> tag { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tag>(entity =>
            {
                entity.HasKey(tag => tag.Id);

                entity.Property(tag => tag.Nome).IsRequired();
            });
        }



    }
}
