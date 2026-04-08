using Playhouse.Domain.SharedKernel.SeedWork;

namespace Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate
{
    public interface IApplicationSettingsRepository : IRepository
    {
        Task<ApplicationSettings?> GetAsync(CancellationToken cancellation = default);

        Task SaveAsync(ApplicationSettings settings, CancellationToken cancellation = default);
    }
}
