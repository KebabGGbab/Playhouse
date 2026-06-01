using Playhouse.Core.Models.BotActions;
using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.ViewModels.ViewModels.BotActionViewModels;

namespace Playhouse.ViewModels.Visitor
{
    public class ViewModelsBrowserEventVisitor : IBotActionVisitor
    {
        public BotActionViewModel? CurrentViewModel { get; private set; }

        public void Visit(PageCreatedBotAction browserEvent)
        {
            CurrentViewModel = new PageCreatedBotActionViewModel(browserEvent);
        }

        public void Visit(PageClosedBotAction browserEvent)
        {
            CurrentViewModel = new PageClosedBotActionViewModel(browserEvent);
        }

        public void Visit(PageGoToBotAction browserEvent)
        {
            CurrentViewModel = new PageGoToBotActionViewModel(browserEvent);
        }

        public void Visit(BrowserContextClosedBotAction browserEvent)
        {
            CurrentViewModel = new BrowserContextClosedBotActionViewModel(browserEvent);
        }

        public void Visit(LocatorClickBotAction browserEvent)
        {
            CurrentViewModel = new LocatorClickBotActionViewModel(browserEvent);
        }
    }
}
