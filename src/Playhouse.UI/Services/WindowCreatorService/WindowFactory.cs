using Playhouse.Core.Services.BotConstructorService.Abstractions;
using Playhouse.UI.Services.WindowCreatorService.Abstractions;
using Playhouse.UI.Views.Windows;
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

        public BotConstructorWindow CreateBotConstructorWindow(BrowserConfigurationViewModel browser, BotConfigurationViewModel bot)
        {
            BotConstructorViewModel viewModel = new(_botConstructorFactory, browser, bot);
            return new BotConstructorWindow(viewModel);
        }
    }
}
