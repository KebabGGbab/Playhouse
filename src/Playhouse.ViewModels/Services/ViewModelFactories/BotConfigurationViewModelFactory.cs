using Microsoft.EntityFrameworkCore;
using Playhouse.Core.Data;
using Playhouse.Core.Models;
using Playhouse.Core.Services.FileManagerService.Abstractions;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.ViewModelFactories
{
    public class BotConfigurationViewModelFactory : IViewModelFactory<BotConfigurationViewModel, BotConfiguration>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly FileManager<BotConfiguration> _fileManager;

        public BotConfigurationViewModelFactory(IDbContextFactory<ApplicationDbContext> dbFactory, FileManager<BotConfiguration> fileManager)
        {
            ArgumentNullException.ThrowIfNull(dbFactory, nameof(dbFactory));
            ArgumentNullException.ThrowIfNull(fileManager, nameof(fileManager));

            _dbFactory = dbFactory;
            _fileManager = fileManager;
        }

        public BotConfigurationViewModel Create()
        {
            return new BotConfigurationViewModel(_dbFactory, _fileManager);
        }

        public BotConfigurationViewModel Create(BotConfiguration model)
        {
            return new BotConfigurationViewModel(_dbFactory, _fileManager, model);
        }
    }
}
