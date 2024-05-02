using API.Enums;
using Microsoft.EntityFrameworkCore;
using PRMS.Entities;
using System;

namespace PRMS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Appointed> Appointeds { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Report>()
                .Property(e => e.ReportingAs)
                .HasConversion(
                    v => v.ToString(),
                    v => (ReportingAs)Enum.Parse(typeof(ReportingAs), v));
        }
    }
}
