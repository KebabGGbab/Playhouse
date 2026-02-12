using Playhouse.Core.Models.BrowserEvents;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class PageClosedBrowserEventViewModel : BrowserEventViewModel
    {
        protected new PageClosedBrowserEvent Event => (PageClosedBrowserEvent)base.Event;

        public string? ReasonClose
        {
            get => Event.CloseOptions.Reason;
            set => SetProperty(ReasonClose, value, Event, (m, v) => m.CloseOptions.Reason = v);
        }

        public bool RunBeforeUnload
        {
            get => Event.CloseOptions.RunBeforeUnload;
            set => SetProperty(RunBeforeUnload, value, Event, (m, v) => m.CloseOptions.RunBeforeUnload = v);
        }

        public PageClosedBrowserEventViewModel(PageClosedBrowserEvent @event) 
            : base(@event)
        { 
        }
    }
}
