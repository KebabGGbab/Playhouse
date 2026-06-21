using Playhouse.Domain;

namespace Playhouse.Infrastructure.Repository
{
    public interface IBrowserConfigurationRepository
    {
        Task<IEnumerable<BrowserConfiguration>> GetAllAsync(CancellationToken cancellation = default);

        Task SaveAsync(BrowserConfiguration configuration, CancellationToken cancellation = default);

        Task SaveAsync(IEnumerable<BrowserConfiguration> configurations, CancellationToken cancellation = default);

        Task DeleteAsync(BrowserConfiguration configuration, CancellationToken cancellation = default);
    }
}
