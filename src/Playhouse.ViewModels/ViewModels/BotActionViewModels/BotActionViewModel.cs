using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public abstract class BotActionViewModel : EditableViewModel
    {
        public abstract int Id { get; }

        protected override bool CanSaveChanges() => IsModified;

        protected override bool CanCancelChanges() => IsModified;
    }
}
