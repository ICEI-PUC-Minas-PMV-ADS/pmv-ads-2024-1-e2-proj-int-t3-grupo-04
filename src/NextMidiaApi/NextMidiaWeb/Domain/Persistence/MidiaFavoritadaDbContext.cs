﻿using Microsoft.EntityFrameworkCore;
using NextMidiaWeb.Domain.Entities;

namespace NextMidiaWeb.Domain.Persistence
{
    public class MidiaFavoritadaDbContext : DbContext
    {
        public MidiaFavoritadaDbContext(DbContextOptions<MidiaFavoritadaDbContext> options) : base(options)
        {

        }

        public DbSet<MidiaFavoritada> midiaFavoritada { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MidiaFavoritada>()
                .HasKey(midiaTag => new { midiaTag.IdMidia, midiaTag.IdUsuario });
        }
    }
}