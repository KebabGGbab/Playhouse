namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public abstract class PageBrowserEvent : BrowserEvent
    {
        public int Number { get; set; }
    }
}
