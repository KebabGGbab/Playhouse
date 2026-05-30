namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public abstract class LocatorBrowserEvent : PageBrowserEvent
    {
        public string? Text { get; set; }
    }
}