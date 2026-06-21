using System.Collections.ObjectModel;
using Playhouse.Domain;

namespace Playhouse.Application.Services.BrowserConfigurationService
{
    public interface IBrowserConfigurationService
    {
        ReadOnlyObservableCollection<BrowserConfiguration> Configurations { get; }

        Task LoadAsync(CancellationToken cancellation = default);

        Task SaveAsync(IEnumerable<BrowserConfiguration> configurations, CancellationToken cancellation = default);

        Task DeleteAsync(BrowserConfiguration configuration, CancellationToken cancellation = default);
    }
}
