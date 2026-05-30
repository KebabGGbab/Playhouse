using Playhouse.Core.Models.BrowserEvents;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class BrowserContextClosedBrowserEventViewModel : BrowserEventViewModel
    {
        protected new BrowserContextClosedBrowserEvent Event => (BrowserContextClosedBrowserEvent)base.Event;

        public string? Reason
        {
            get => Event.CloseOptions.Reason;
            set => SetProperty(Event.CloseOptions.Reason, value, Event, (m, v) => m.CloseOptions.Reason = v);
        }

        public BrowserContextClosedBrowserEventViewModel(BrowserContextClosedBrowserEvent @event) : base(@event)
        {
        }
    }
}
