using Playhouse.Core.Models.BrowserEvents;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class PageCreatedBrowserEventViewModel : BrowserEventViewModel<PageCreatedBrowserEvent>
    {
        public PageCreatedBrowserEventViewModel(PageCreatedBrowserEvent @event) 
            : base(@event)
        {
        }

        protected override bool CheckModified()
        {
            return false;
        }

        protected override Task SaveChangesCoreAsync()
        {
            return Task.CompletedTask;
        }

        protected override void CancelChangesCore()
        {
        }
    }
}
