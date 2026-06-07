using Playhouse.Core.Models.BotActions;
using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.ViewModels.ViewModels.BotActionViewModels;

namespace Playhouse.ViewModels.Visitor
{
    public class ViewModelsBrowserEventVisitor : IBotActionVisitor<BotActionViewModel>
    {
        public BotActionViewModel? CurrentViewModel { get; private set; }

        public BotActionViewModel Visit(BrowserContextClosedBotAction action)
        {
            return new BrowserContextClosedBotActionViewModel(action);
        }

        public BotActionViewModel Visit(PageCreatedBotAction action)
        {
            return new PageCreatedBotActionViewModel(action);
        }

        public BotActionViewModel Visit(PageClosedBotAction action)
        {
            return new PageClosedBotActionViewModel(action);
        }

        public BotActionViewModel Visit(PageGoToBotAction action)
        {
            return new PageGoToBotActionViewModel(action);
        }

        public BotActionViewModel Visit(LocatorClickBotAction action)
        {
            return new LocatorClickBotActionViewModel(action);
        }

        public void Visit(LocatorClickBotAction browserEvent)
        {
            CurrentViewModel = new LocatorClickBotActionViewModel(browserEvent);
        }
    }
}
