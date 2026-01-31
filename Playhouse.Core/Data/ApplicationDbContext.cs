using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data.EntityTypeConfigurations;
using Playhouse.Core.Models;
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));

            new BrowserProfileEntityTypeConfiguration().Configure(modelBuilder.Entity<BrowserProfile>());
            new BotInfoEntityTypeConfiguration().Configure(modelBuilder.Entity<BotInfo>());
            new BrowserEventEntityTypeConfiguration().Configure(modelBuilder.Entity<BrowserEvent>());
        }
    }
}
