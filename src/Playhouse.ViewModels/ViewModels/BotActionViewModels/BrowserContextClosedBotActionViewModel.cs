using Playhouse.Core.Models.BotActions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public class BrowserContextClosedBotActionViewModel : BotActionViewModel<BrowserContextClosedBotAction>
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

        public BrowserContextClosedBotActionViewModel(BrowserContextClosedBotAction action) 
            : base(action)
        {
            _reason = action.Options.Reason;
        }

        protected override bool CheckModified()
        {
            return !(_reason == Action.Options.Reason);
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Action.Options.Reason = _reason;
        }

        protected override void CancelChangesCore()
        {
            Reason = Action.Options.Reason;
        }
    }
}
