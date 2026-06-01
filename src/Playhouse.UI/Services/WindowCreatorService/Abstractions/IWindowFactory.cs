using Playhouse.UI.Views.Windows;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Services.WindowCreatorService.Abstractions
{
    internal interface IWindowFactory
    {
        BotConstructorWindow CreateBotConstructorWindow(BrowserConfigurationViewModel browser, BotConfigurationViewModel bot);
    }
}
