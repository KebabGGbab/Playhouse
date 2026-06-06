using Playhouse.Core.Models;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.ViewModelFactories
{
    public class BrowserConfigurationViewModelFactory : IViewModelFactory<BrowserConfigurationViewModel, BrowserConfiguration>
    {
        private readonly FileManager<BrowserConfiguration> _fileManager;

        public BrowserConfigurationViewModelFactory(FileManager<BrowserConfiguration> fileManager)
        {
            ArgumentNullException.ThrowIfNull(fileManager);

            _fileManager = fileManager;
        }

        public BrowserConfigurationViewModel Create()
        {
            return new BrowserConfigurationViewModel(_fileManager);
        }

        public BrowserConfigurationViewModel Create(BrowserConfiguration model)
        {
            return new BrowserConfigurationViewModel(model, _fileManager);
        }
    }
}
