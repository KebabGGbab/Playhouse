using System.Collections.ObjectModel;
using Playhouse.Application.Services.FileManagerService.Abstractions;
using Playhouse.Domain;
using Playhouse.Infrastructure.Repository;

namespace Playhouse.Application.Services.BrowserConfigurationService
{
    public sealed class BrowserConfigurationService : IBrowserConfigurationService, IInitializer
    {
        // зависимости
        private readonly IBrowserConfigurationRepository _repository;
        private readonly FileManager<BrowserConfiguration> _fileManager;

        private readonly ObservableCollection<BrowserConfiguration> _configurations;

        public bool IsInitialized { get; private set; }

        public ReadOnlyObservableCollection<BrowserConfiguration> Configurations { get; }

        public BrowserConfigurationService(IBrowserConfigurationRepository repository, FileManager<BrowserConfiguration> fileManager)
        {
            ArgumentNullException.ThrowIfNull(repository);
            ArgumentNullException.ThrowIfNull(fileManager);

            _repository = repository;
            _fileManager = fileManager;
            _configurations = [];
            Configurations = new ReadOnlyObservableCollection<BrowserConfiguration>(_configurations);
        }

        public async Task InitializeAsync(CancellationToken cancellation = default)
        {
            await LoadAsync(cancellation)
                .ConfigureAwait(false);
            IsInitialized = true;
        }

        public async Task LoadAsync(CancellationToken cancellation = default)
        {
            // Пока что так, потом при необходимости будет изменено на оптимизированный вариант
            _configurations.Clear();

            foreach (BrowserConfiguration configuration in await _repository.GetAllAsync(cancellation).ConfigureAwait(false))
            {
                _configurations.Add(configuration);
            }
        }

        public async Task DeleteAsync(BrowserConfiguration configuration, CancellationToken cancellation = default)
        {
            ThrowIfNotInitialized();

            await _repository.DeleteAsync(configuration, cancellation)
                .ConfigureAwait(false);
            _configurations.Remove(configuration);
            _fileManager.Delete(configuration);
        }

        public async Task SaveAsync(IEnumerable<BrowserConfiguration> configurations, CancellationToken cancellation = default)
        {
            ThrowIfNotInitialized();

            List<BrowserConfiguration> newConfigurations = configurations.Where(c => c.Id == default).ToList();
            await _repository.SaveAsync(configurations, cancellation)
                .ConfigureAwait(false);

            foreach (BrowserConfiguration configuration in newConfigurations)
            {
                _fileManager.Create(configuration);
                _configurations.Add(configuration);
            }
        }

        private void ThrowIfNotInitialized()
        {
            if (!IsInitialized)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
