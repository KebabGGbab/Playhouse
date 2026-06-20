using Playhouse.Domain;

namespace Playhouse.Infrastructure.Repository
{
    public interface ISettingsRepository
    {
        Task<ApplicationSettings?> GetSettingsAsync(CancellationToken cancellation = default);

        Task UpdateSettingsAsync(ApplicationSettings settings, CancellationToken cancellation = default);
    }
}
