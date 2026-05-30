namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public interface IBrowserEventVisitor
    {
        void Visit(PageCreatedBrowserEvent browserEvent);
        void Visit(PageClosedBrowserEvent browserEvent);
        void Visit(PageGoToBrowserEvent browserEvent);
        void Visit(BrowserContextClosedBrowserEvent browserEvent);
        void Visit(LocatorClickBrowserEvent browserEvent);
    }
}
