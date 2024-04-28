using NextMidiaApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NextMidiaApi.Domain.Persistence
{
    public class TagDbContext : DbContext
    {
        public TagDbContext(DbContextOptions<TagDbContext> options) : base(options)
        {

        }

        public DbSet<Tag> tags { get; set; }

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
