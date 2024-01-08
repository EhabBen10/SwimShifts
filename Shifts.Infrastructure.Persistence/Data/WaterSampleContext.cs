using Shifts.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shifts.Application.Models;

namespace Shifts.Infrastructure.Persistence.Data
{
    public class WaterSampleContext : DbContext, IApplicationDbContext
    {
        // TODO: Define DbSet properties for your entities
        public DbSet<WaterSample> WaterSamples { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public WaterSampleContext(DbContextOptions<WaterSampleContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WaterSample>().Property(b => b.AutoFritKlor).HasPrecision(18, 6);
            modelBuilder.Entity<WaterSample>().Property(b => b.AutoPH).HasPrecision(18, 6);
            modelBuilder.Entity<WaterSample>().Property(b => b.Ph).HasPrecision(18, 6);
            modelBuilder.Entity<WaterSample>().Property(b => b.FritKlor).HasPrecision(18, 6);
            modelBuilder.Entity<WaterSample>().Property(b => b.Differace).HasPrecision(18, 6);
            modelBuilder.Entity<WaterSample>().Property(b => b.Bundklor).HasPrecision(18, 6);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WaterSampleContext).Assembly);
        }
    }
}
