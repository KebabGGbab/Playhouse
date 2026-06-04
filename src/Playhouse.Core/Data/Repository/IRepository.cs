using Playhouse.Core.Services.ApplicationSettingsService;

namespace Playhouse.Core.Data.Repository
{
    public interface ISettingsRepository
    {
        Task<ApplicationSettings?> GetSettingsAsync();

        Task UpdateSettingsAsync(ApplicationSettings settings);
    }
}
