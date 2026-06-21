using Microsoft.EntityFrameworkCore;
using Playhouse.Domain;

namespace Playhouse.Infrastructure.Repository
{
    public sealed class SettingsRepository : ISettingsRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public SettingsRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            ArgumentNullException.ThrowIfNull(dbFactory);

            _dbFactory = dbFactory;
        }

        public async Task<ApplicationSettings?> GetSettingsAsync(CancellationToken cancellation = default)
        {
            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync(cancellation).ConfigureAwait(false);

            return await db.Settings.FirstOrDefaultAsync(s => s.Id == 1, cancellation).ConfigureAwait(false);
        }

        public async Task UpdateSettingsAsync(ApplicationSettings settings, CancellationToken cancellation = default)
        {
            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync(cancellation).ConfigureAwait(false);
            db.Settings.Update(settings);
            await db.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }
    }
}
