using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public abstract class BotActionViewModel : EditableViewModel
    {
        public abstract int Id { get; }
    }
}
