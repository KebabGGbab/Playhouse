using Playhouse.Core.Services.BotConstructorService.Abstractions;
using Playhouse.UI.Services.WindowCreatorService.Abstractions;
using Playhouse.UI.Views;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Services.WindowCreatorService
{
    internal sealed class WindowFactory : IWindowFactory
    {
        private readonly IBotConstructorFactory _botConstructorFactory;

        public WindowFactory(IBotConstructorFactory botConstructorFactory)
        {
            _botConstructorFactory = botConstructorFactory;
        }

        public BotConstructorWindow CreateBotConstructorWindow(BrowserProfileViewModel profile, BotInfoViewModel botInfo)
        {
            BotConstructorViewModel viewModel = new(_botConstructorFactory, profile, botInfo);
            return new BotConstructorWindow(viewModel);
        }
    }
}
