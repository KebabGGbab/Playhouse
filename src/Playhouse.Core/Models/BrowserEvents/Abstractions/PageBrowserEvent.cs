namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public abstract class PageBrowserEvent : BrowserEvent
    {
        public required int Number { get; set; }
    }
}
