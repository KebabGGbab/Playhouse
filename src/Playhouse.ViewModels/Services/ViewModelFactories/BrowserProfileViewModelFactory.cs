using Playhouse.Core.Models;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.ViewModelFactories
{
    public class BrowserProfileViewModelFactory : IViewModelFactory<BrowserProfileViewModel, BrowserProfile>
    {
        private readonly FileManager<BrowserProfile> _fileManager;

        public BrowserProfileViewModelFactory(FileManager<BrowserProfile> fileManager)
        {
            _fileManager = fileManager;
        }

        public BrowserProfileViewModel Create()
        {
            return new BrowserProfileViewModel(_fileManager);
        }

        public BrowserProfileViewModel Create(BrowserProfile model)
        {
            return new BrowserProfileViewModel(model, _fileManager);
        }
    }
}
