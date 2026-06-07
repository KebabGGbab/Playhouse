using Playhouse.Core.Models.BotActions;
using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.ViewModels.ViewModels.BotActionViewModels;

namespace Playhouse.ViewModels.Visitor
{
    public class ViewModelsBrowserEventVisitor : IBotActionVisitor<BotActionViewModel>
    {
        public BotActionViewModel Visit(BrowserContextCreatedBotAction action)
        {
            return new BrowserContextCreatedBotActionViewModel(action);
        }

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

        public BotActionViewModel Visit(LocatorFillBotAction action)
        {
            return new LocatorFillBotActionViewModel(action);
        }
    }
}
