using Microsoft.EntityFrameworkCore;
using Playhouse.Domain;

namespace Playhouse.Infrastructure.Repository
{
    public sealed class BrowserConfigurationRepository : IBrowserConfigurationRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        
        public BrowserConfigurationRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            ArgumentNullException.ThrowIfNull(dbFactory);

            _dbFactory = dbFactory;
        }

        public async Task DeleteAsync(BrowserConfiguration configuration, CancellationToken cancellation = default)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync(cancellation).ConfigureAwait(false);
            db.Profiles.Remove(configuration);
            await db.SaveChangesAsync(cancellation);
        }

        public async Task<IEnumerable<BrowserConfiguration>> GetAllAsync(CancellationToken cancellation = default)
        {
            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync(cancellation).ConfigureAwait(false);

            return await db.Profiles.ToListAsync(cancellation).ConfigureAwait(false);
        }

        public async Task SaveAsync(BrowserConfiguration configuration, CancellationToken cancellation = default)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync(cancellation).ConfigureAwait(false);
            AddOrUpdate(configuration, db);
            await db.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task SaveAsync(IEnumerable<BrowserConfiguration> configurations, CancellationToken cancellation = default)
        {
            ArgumentNullException.ThrowIfNull(configurations);

            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync(cancellation).ConfigureAwait(false);

            foreach (BrowserConfiguration configuration in configurations)
            {
                AddOrUpdate(configuration, db);
            }

            await db.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        private static void AddOrUpdate(BrowserConfiguration configuration, ApplicationDbContext db)
        {
            if (configuration.Id == default)
            {
                db.Profiles.Add(configuration);
            }
            else
            {
                db.Profiles.Update(configuration);
            }
        }
    }
}
