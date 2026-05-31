using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public abstract class BrowserEventViewModel : EditableViewModel
    {
        public abstract int Id { get; }
    }
}
