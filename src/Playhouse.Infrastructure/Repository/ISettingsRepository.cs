using Playhouse.Domain;

namespace Playhouse.Infrastructure.Repository
{
    public interface ISettingsRepository
    {
        Task<ApplicationSettings?> GetSettingsAsync();

        Task UpdateSettingsAsync(ApplicationSettings settings);
    }
}
