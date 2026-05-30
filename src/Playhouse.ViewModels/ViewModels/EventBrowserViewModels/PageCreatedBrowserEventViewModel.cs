using Playhouse.Core.Models.BrowserEvents;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class PageCreatedBrowserEventViewModel : BrowserEventViewModel
    {
        protected new PageCreatedBrowserEvent Event => (PageCreatedBrowserEvent)base.Event; 

        public PageCreatedBrowserEventViewModel(PageCreatedBrowserEvent @event) 
            : base(@event)
        {
        }
    }
}
