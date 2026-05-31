using Playhouse.Core.Models.BrowserEvents;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class BrowserContextClosedBrowserEventViewModel : BrowserEventViewModel<BrowserContextClosedBrowserEvent>
    {
        private string? _reason;

        public string? Reason
        {
            get => _reason;
            set
            {
                if (SetProperty(ref _reason, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public BrowserContextClosedBrowserEventViewModel(BrowserContextClosedBrowserEvent @event) : base(@event)
        {
            _reason = @event.Options.Reason;
        }

        protected override bool CheckModified()
        {
            return !(_reason == Event.Options.Reason);
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Event.Options.Reason = _reason;
        }

        protected override void CancelChangesCore()
        {
            Reason = Event.Options.Reason;
        }
    }
}
