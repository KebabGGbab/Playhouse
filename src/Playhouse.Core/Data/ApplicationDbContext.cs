using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data.EntityTypeConfigurations;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BrowserProfile> BrowserProfiles { get; set; }

        public DbSet<BotInfo> BotsInfo { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));

            new BrowserProfileEntityTypeConfiguration().Configure(modelBuilder.Entity<BrowserProfile>());
            new BotInfoEntityTypeConfiguration().Configure(modelBuilder.Entity<BotInfo>());
            new BrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<BrowserEvent>());
            new BrowserContextBrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<BrowserContextBrowserEvent>());
            new BrowserContextClosedBrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<BrowserContextClosedBrowserEvent>());
            new PageBrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<PageBrowserEvent>());
            new PageCreatedBrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<PageCreatedBrowserEvent>());
            new PageClosedBrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<PageClosedBrowserEvent>());
            new PageGoToBrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<PageGoToBrowserEvent>());
            new LocatorBrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<LocatorBrowserEvent>());
            new LocatorClickBrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<LocatorClickBrowserEvent>());
        }
    }
}
