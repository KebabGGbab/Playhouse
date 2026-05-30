using Playhouse.UI.Views.Windows;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Services.WindowCreatorService.Abstractions
{
    internal interface IWindowFactory
    {
        BotConstructorWindow CreateBotConstructorWindow(BrowserProfileViewModel profile, BotInfoViewModel botInfo);
    }
}
