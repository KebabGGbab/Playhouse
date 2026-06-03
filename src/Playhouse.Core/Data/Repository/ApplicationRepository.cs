using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Services.ApplicationSettingsService;

namespace Playhouse.Core.Data.Repository
{
    public sealed class ApplicationRepository : ISettingsRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ApplicationRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            ArgumentNullException.ThrowIfNull(dbFactory);

            _dbFactory = dbFactory;
        }

        public async Task<ApplicationSettings> GetSettingsAsync()
        {
            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync().ConfigureAwait(false);
            return await db.Settings.FirstAsync(s => s.Id == 0).ConfigureAwait(false);
        }

        public async Task UpdateSettingsAsync(ApplicationSettings settings)
        {
            using ApplicationDbContext db = await _dbFactory.CreateDbContextAsync().ConfigureAwait(false);
            db.Settings.Update(settings);
            await db.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
