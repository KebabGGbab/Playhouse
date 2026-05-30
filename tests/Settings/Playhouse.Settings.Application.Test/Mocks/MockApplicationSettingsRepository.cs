using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate;

namespace Playhouse.Application.Test.Mocks
{
    internal class MockApplicationSettingsRepository : IApplicationSettingsRepository
    {
        private readonly ApplicationSettings? _settings;

        public MockApplicationSettingsRepository(ApplicationSettings? settings = null)
        {
            _settings = settings;
        }

        public async Task<ApplicationSettings?> GetAsync(CancellationToken cancellation = default)
        {
            await Task.Delay(1000, cancellation).ConfigureAwait(false);

            return _settings;
        }

        public async Task SaveAsync(ApplicationSettings settings, CancellationToken cancellation = default)
        {
            await Task.Delay(1000, cancellation).ConfigureAwait(false);
        }
    }
}
