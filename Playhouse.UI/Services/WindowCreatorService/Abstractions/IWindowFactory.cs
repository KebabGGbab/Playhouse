using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.UI.Views;

namespace Playhouse.UI.Services.WindowCreatorService.Abstractions
{
    internal interface IWindowFactory
    {
        BotConstructorWindow CreateBotConstructorWindow(BrowserProfile profile, string botName, BrowserType browser);
    }
}
