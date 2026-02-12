using CommunityToolkit.Mvvm.ComponentModel;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class BrowserEventViewModel : ObservableObject
    {
        private readonly BrowserEvent _event;

        protected BrowserEvent Event => _event;

        public int Id => _event.Id;

        public BrowserEventViewModel(BrowserEvent @event)
        {
            ArgumentNullException.ThrowIfNull(@event, nameof(@event));

            _event = @event;
        }
    }
}
