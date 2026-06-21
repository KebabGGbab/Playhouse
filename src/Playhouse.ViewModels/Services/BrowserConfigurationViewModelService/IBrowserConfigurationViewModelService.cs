using System.Collections.ObjectModel;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.BrowserConfigurationViewModelService
{
    public interface IBrowserConfigurationViewModelService
    {
        ReadOnlyObservableCollection<BrowserConfigurationViewModel> Configurations { get; }

        Task LoadAsync(CancellationToken cancellation = default);

        Task SaveAsync(IEnumerable<BrowserConfigurationViewModel> configurations, CancellationToken cancellation = default);

        Task DeleteAsync(BrowserConfigurationViewModel configuration, CancellationToken cancellation = default);
    }
}
