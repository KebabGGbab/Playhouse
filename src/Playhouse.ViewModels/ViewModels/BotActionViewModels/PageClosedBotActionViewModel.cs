using Playhouse.Domain.BotActions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public class PageClosedBotActionViewModel : BotActionViewModel<PageClosedBotAction>
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

        public PageClosedBotActionViewModel(PageClosedBotAction action) 
            : base(action)
        {
            _reason = action.Options.Reason;
            _runBeforeUnload = action.Options.RunBeforeUnload;
        }

        protected override bool CheckModified()
        {
            return !(_reason == Action.Options.Reason
                && _runBeforeUnload == Action.Options.RunBeforeUnload);
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Action.Options.Reason = _reason;
            Action.Options.RunBeforeUnload = _runBeforeUnload;
        }

        protected override void CancelChangesCore()
        {
            Reason = Action.Options.Reason;
            RunBeforeUnload = Action.Options.RunBeforeUnload;
        }
    }
}
