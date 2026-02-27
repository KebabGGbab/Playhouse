using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Models;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.ViewModelFactories
{
    public class BrowserProfileViewModelFactory : IViewModelFactory<BrowserProfileViewModel, BrowserProfile>
    {
        private readonly FileManager<BrowserProfile> _fileManager;
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public BrowserProfileViewModelFactory(FileManager<BrowserProfile> fileManager, IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _fileManager = fileManager;
            _dbFactory = dbFactory;
        }

        public BrowserProfileViewModel Create()
        {
            return new BrowserProfileViewModel(_dbFactory, _fileManager);
        }

        public BrowserProfileViewModel Create(BrowserProfile model)
        {
            return new BrowserProfileViewModel(model, _dbFactory, _fileManager);
        }
    }
}
