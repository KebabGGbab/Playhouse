using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data.EntityTypeConfigurations;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BrowserConfiguration> Profiles { get; set; }

        public DbSet<BotConfiguration> Bots { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new BrowserConfigurationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BotConfigurationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BrowserContextBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BrowserContextClosedBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageCreatedBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageClosedBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PageGoToBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocatorBotActionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocatorClickBotActionEntityTypeConfiguration());
        }
    }
}
