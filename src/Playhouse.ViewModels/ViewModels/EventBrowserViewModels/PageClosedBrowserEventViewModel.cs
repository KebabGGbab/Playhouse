using Playhouse.Core.Models.BrowserEvents;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class PageClosedBrowserEventViewModel : BrowserEventViewModel<PageClosedBrowserEvent>
    {
        private string? _reason;
        private bool _runBeforeUnload;

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

        public bool RunBeforeUnload
        {
            get => _runBeforeUnload;
            set
            {
                if (SetProperty(ref _runBeforeUnload, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public PageClosedBrowserEventViewModel(PageClosedBrowserEvent @event) 
            : base(@event)
        {
            _reason = @event.Options.Reason;
            _runBeforeUnload = @event.Options.RunBeforeUnload;
        }

        protected override bool CheckModified()
        {
            return !(_reason == Event.Options.Reason
                && _runBeforeUnload == Event.Options.RunBeforeUnload);
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Event.Options.Reason = _reason;
            Event.Options.RunBeforeUnload = _runBeforeUnload;
        }

        protected override void CancelChangesCore()
        {
            Reason = Event.Options.Reason;
            RunBeforeUnload = Event.Options.RunBeforeUnload;
        }
    }
}
