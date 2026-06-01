using Playhouse.Core.Models.BotActions.Abstractions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public abstract class BotActionViewModel<T> : BotActionViewModel
        where T : BotAction
    {
        protected T Action { get; }

        public override int Id => Action.Id;

        protected BotActionViewModel(T action)
        {
            ArgumentNullException.ThrowIfNull(action);

            Action = action;
        }
    }
}
