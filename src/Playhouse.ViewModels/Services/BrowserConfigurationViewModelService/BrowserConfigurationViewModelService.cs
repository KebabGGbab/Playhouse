using System.Collections.ObjectModel;
using DynamicData;
using DynamicData.Binding;
using Playhouse.Application.Services.BrowserConfigurationService;
using Playhouse.Domain;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.BrowserConfigurationViewModelService
{
    public sealed class BrowserConfigurationViewModelService : IBrowserConfigurationViewModelService, IDisposable
    {
        // зависимости
        private readonly IBrowserConfigurationService _browserConfigurationService;

        private readonly SourceList<BrowserConfiguration> _source;
        private readonly ReadOnlyObservableCollection<BrowserConfigurationViewModel> _configurations;
        private readonly IDisposable _bindingSource;

        private bool _disposed;

        public ReadOnlyObservableCollection<BrowserConfigurationViewModel> Configurations => _configurations;

        public event EventHandler<IBrowserConfigurationViewModelService, EventArgs>? ConfigurationsChanged;

        public BrowserConfigurationViewModelService(IBrowserConfigurationService browserConfigurationService)
        {
            ArgumentNullException.ThrowIfNull(browserConfigurationService);

            _browserConfigurationService = browserConfigurationService;
            _source = new SourceList<BrowserConfiguration>();
            _bindingSource = _browserConfigurationService.Configurations
                .ToObservableChangeSet()
                .PopulateInto(_source);
            _source.Connect()
                .Transform(c => new BrowserConfigurationViewModel(c))
                .Bind(out _configurations)
                .Subscribe();
        }

        public async Task LoadAsync(CancellationToken cancellation = default)
        {
            Validate();

            await _browserConfigurationService.LoadAsync(cancellation)
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(BrowserConfigurationViewModel configuration, CancellationToken cancellation = default)
        {
            Validate();

            await _browserConfigurationService.DeleteAsync(configuration.Profile, cancellation)
                .ConfigureAwait(false);
        }

        public async Task SaveAsync(IEnumerable<BrowserConfigurationViewModel> configurations, CancellationToken cancellation = default)
        {
            Validate();

            await _browserConfigurationService.SaveAsync(configurations.Select(c => c.Profile), cancellation)
                .ConfigureAwait(false);
        }

        private void Validate()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            GC.SuppressFinalize(this);
            _bindingSource.Dispose();
        }
    }
}
