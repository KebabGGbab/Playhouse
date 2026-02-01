namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public abstract class BrowserContextBrowserEvent : BrowserEvent
    {
        public required int Number { get; init; }
    }
}
