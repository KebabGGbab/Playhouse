using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Models;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.ViewModelFactories
{
    public class BotInfoViewModelFactory : IViewModelFactory<BotInfoViewModel, BotInfo>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly FileManager<BotInfo> _fileManager;

        public BotInfoViewModelFactory(IDbContextFactory<ApplicationDbContext> dbFactory, FileManager<BotInfo> fileManager)
        {
            ArgumentNullException.ThrowIfNull(dbFactory, nameof(dbFactory));
            ArgumentNullException.ThrowIfNull(fileManager, nameof(fileManager));

            _dbFactory = dbFactory;
            _fileManager = fileManager;
        }

        public BotInfoViewModel Create()
        {
            return new BotInfoViewModel(_dbFactory, _fileManager);
        }

        public BotInfoViewModel Create(BotInfo model)
        {
            return new BotInfoViewModel(_dbFactory, _fileManager, model);
        }
    }
}
