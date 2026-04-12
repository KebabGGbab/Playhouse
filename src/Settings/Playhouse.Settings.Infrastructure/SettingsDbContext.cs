using Microsoft.EntityFrameworkCore;
using Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate;
using Playhouse.Settings.Infrastructure.EntityConfigurations;

namespace Playhouse.Settings.Infrastructure
{
    public sealed class SettingsDbContext : DbContext
    {
        public DbSet<ApplicationSettings> ApplicationSettings { get; set; }

        public SettingsDbContext(DbContextOptions<SettingsDbContext> options)
            : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationsSettingsEntityTypeConfiguration());
        }
    }
}
