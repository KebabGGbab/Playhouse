using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public abstract class BrowserEventViewModel<T> : BrowserEventViewModel
        where T : BrowserEvent
    {
        protected T Event { get; }

        public override int Id => Event.Id;

        protected BrowserEventViewModel(T @event)
        {
            ArgumentNullException.ThrowIfNull(@event);

            Event = @event;
        }
    }
}
