using Playhouse.SharedKernel.Domain.Data;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate
{
    public interface IApplicationSettingsRepository : IRepository
    {
        Task<ApplicationSettings?> GetAsync(CancellationToken cancellation = default);

        Task SaveAsync(ApplicationSettings settings, CancellationToken cancellation = default);
    }
}
