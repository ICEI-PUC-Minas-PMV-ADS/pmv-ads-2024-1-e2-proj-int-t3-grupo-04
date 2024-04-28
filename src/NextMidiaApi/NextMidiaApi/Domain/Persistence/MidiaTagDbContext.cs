using NextMidiaApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace NextMidiaApi.Domain.Persistence
{
    public class MidiaTagDbContext : DbContext
    {
        public MidiaTagDbContext(DbContextOptions<MidiaTagDbContext> options) : base(options)
        {

        }

        public DbSet<MidiaTag> MidiaTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MidiaTag>()
                .HasKey(midiaTag => new { midiaTag.Midia, midiaTag.Tag });
        }



    }
}
