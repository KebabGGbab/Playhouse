using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class PageGoToBrowserEventViewModel : BrowserEventViewModel
    {
        protected new PageGoToBrowserEvent Event => (PageGoToBrowserEvent)base.Event;

        public string Url
        {
            get => Event.Url.ToString();
            set => SetProperty(Event.Url.ToString(), value, Event, (m, v) => m.Url = new Uri(v));
        }

        public string? Referer
        {
            get => Event.GotoOptions.Referer;
            set => SetProperty(Event.GotoOptions.Referer, value, Event, (m, v) => m.GotoOptions.Referer = v);
        }

        public float Timeout
        {
            get => Event.GotoOptions.Timeout;
            set => SetProperty(Event.GotoOptions.Timeout, value, Event, (m, v) => m.GotoOptions.Timeout = v);
        }

        public WaitUntilState WaitUntil
        {
            get => Event.GotoOptions.WaitUntil;
            set => SetProperty(Event.GotoOptions.WaitUntil, value, Event, (m, v) => m.GotoOptions.WaitUntil = v);
        }

        public PageGoToBrowserEventViewModel(PageGoToBrowserEvent @event)
            : base(@event)
        {
        }
    }
}
