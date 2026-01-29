using System.Collections.ObjectModel;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.Core.Services.BotConstructorService.Abstractions;
using Playhouse.UI.Services.WindowCreatorService.Abstractions;
using Playhouse.UI.Views;
using Playhouse.ViewModels.ViewModels;

namespace Playhouse.UI.Services.WindowCreatorService
{
    internal sealed class WindowFactory : IWindowFactory
    {
        private readonly IBotConstructor _botConstructor;

        public WindowFactory(IBotConstructor botConstructor)
        { 
            _botConstructor = botConstructor;
        }

        public BotConstructorWindow CreateBotConstructorWindow(BrowserProfile profile, string botName, BrowserType browser)
        {
            BotConstructorViewModel viewModel = new(_botConstructor, profile, new BotInfo(botName, 0, browser) { BrowserEvents = new ObservableCollection<BrowserEvent>() });
            return new BotConstructorWindow(viewModel);
        }
    }
}
