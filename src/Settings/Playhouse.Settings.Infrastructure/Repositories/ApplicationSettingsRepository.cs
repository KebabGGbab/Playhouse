using Microsoft.EntityFrameworkCore;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;

namespace Playhouse.Settings.Infrastructure.Repositories
{
    public sealed class ApplicationSettingsRepository : IApplicationSettingsRepository
    {
        private readonly IDbContextFactory<SettingsDbContext> _dbContextFactory;

        public ApplicationSettingsRepository(IDbContextFactory<SettingsDbContext> dbContextFactory) 
        {
            ArgumentNullException.ThrowIfNull(dbContextFactory);

            _dbContextFactory = dbContextFactory;
        }

        public async Task<ApplicationSettings?> GetAsync(CancellationToken cancellation = default)
        {
            using SettingsDbContext dbContext = await CreateContextAsync(cancellation);

            return await dbContext.ApplicationSettings.FirstOrDefaultAsync(cancellation);
        }

        public async Task SaveAsync(ApplicationSettings settings, CancellationToken cancellation = default)
        {
            using SettingsDbContext dbContext = await CreateContextAsync(cancellation);
            dbContext.Attach(settings);
            await dbContext.SaveChangesAsync(cancellation);
        }

        private async Task<SettingsDbContext> CreateContextAsync(CancellationToken cancellation = default)
        {
            return await _dbContextFactory.CreateDbContextAsync(cancellation);
        }
    }
}
