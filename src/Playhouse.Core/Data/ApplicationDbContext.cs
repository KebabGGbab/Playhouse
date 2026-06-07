using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data.EntityTypeConfigurations;
using Playhouse.Core.Models;
using Playhouse.Core.Services.ApplicationSettingsService;

namespace Playhouse.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BrowserConfiguration> Profiles { get; set; }

        public DbSet<BotConfiguration> Bots { get; set; }

        public DbSet<ApplicationSettings> Settings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationSettingsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BrowserConfigurationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BotConfigurationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BrowserContextBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BrowserContextCreatedBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BrowserContextClosedBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageCreatedBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageClosedBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageGoToBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocatorBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocatorClickBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocatorFillBotActionEntityTypeConfiguration());
        }
    }
}
