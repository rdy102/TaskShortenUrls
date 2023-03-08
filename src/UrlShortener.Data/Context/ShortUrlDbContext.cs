using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Entities;

namespace UrlShortener.Data.Context
{
    public class ShortUrlDbContext :DbContext
    {
        public ShortUrlDbContext(DbContextOptions<ShortUrlDbContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortUrl>()
                .HasIndex(s => s.Alias)
                .IsUnique();
        }

        public DbSet<ShortUrl> shortUrls { get; set; }
    }
}