using Playhouse.Domain;
using Playhouse.ViewModels.Services.ViewModelFactories.Abstractions;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.ViewModels.Services.ViewModelFactories
{
    public class BotConfigurationViewModelFactory : IViewModelFactory<BotConfigurationViewModel, BotConfiguration>
    {
        public BotConfigurationViewModel Create()
        {
            return new BotConfigurationViewModel();
        }

        public BotConfigurationViewModel Create(BotConfiguration model)
        {
            return new BotConfigurationViewModel(model);
        }
    }
}
