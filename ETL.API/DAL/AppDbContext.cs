﻿using ETL.API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL.API.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ETLData> ETLData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ETLData>().ToTable("ETLData")
                 .HasIndex(p => new { p.PULocationID, p.TipAmount });
        }
    }
}
