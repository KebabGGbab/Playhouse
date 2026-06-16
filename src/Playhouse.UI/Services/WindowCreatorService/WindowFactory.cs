using Playhouse.Application.Services.ConstructorService.Abstractions;
using Playhouse.UI.Services.WindowCreatorService.Abstractions;
using Playhouse.UI.Views.Windows;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Services.WindowCreatorService
{
    internal sealed class WindowFactory : IWindowFactory
    {
        private readonly IConstructorFactory _botConstructorFactory;

        public WindowFactory(IConstructorFactory botConstructorFactory)
        {
            ArgumentNullException.ThrowIfNull(botConstructorFactory);

            _botConstructorFactory = botConstructorFactory;
        }

        public BotConstructorWindow CreateBotConstructorWindow(BrowserConfigurationViewModel browser, BotConfigurationViewModel bot)
        {
            BotConstructorViewModel viewModel = new(_botConstructorFactory, browser, bot);

            return new BotConstructorWindow(viewModel);
        }
    }
}
