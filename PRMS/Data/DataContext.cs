using Microsoft.EntityFrameworkCore;
using PRMS.Entities;

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
    }
}
