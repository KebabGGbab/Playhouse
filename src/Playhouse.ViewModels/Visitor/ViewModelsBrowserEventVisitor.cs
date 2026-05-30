using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Models.BrowserEvents.Abstractions;
using Playhouse.ViewModels.ViewModels.EventBrowserViewModels;

namespace Playhouse.ViewModels.Visitor
{
    public class ViewModelsBrowserEventVisitor : IBrowserEventVisitor
    {
        public BrowserEventViewModel? CurrentViewModel { get; private set; }

        public void Visit(PageCreatedBrowserEvent browserEvent)
        {
            CurrentViewModel = new BrowserEventViewModel(browserEvent);
        }

        public void Visit(PageClosedBrowserEvent browserEvent)
        {
            CurrentViewModel = new PageClosedBrowserEventViewModel(browserEvent);
        }

        public void Visit(PageGoToBrowserEvent browserEvent)
        {
            CurrentViewModel = new PageGoToBrowserEventViewModel(browserEvent);
        }

        public void Visit(BrowserContextClosedBrowserEvent browserEvent)
        {
            CurrentViewModel = new BrowserContextClosedBrowserEventViewModel(browserEvent);
        }

        public void Visit(LocatorClickBrowserEvent browserEvent)
        {
            CurrentViewModel = new LocatorClickBrowserEventViewModel(browserEvent);
        }
    }
}
