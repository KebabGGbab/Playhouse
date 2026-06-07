using Playhouse.Core.Models.BotActions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public class LocatorFillBotActionViewModel : BotActionViewModel<LocatorFillBotAction>
    {
        private string _value;
        private bool _force;
        private float _timeout;

        public string Value
        {
            get => _value;
            set
            {
                if (SetProperty(ref _value, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public bool Force
        {
            get => _force;
            set
            {
                if (SetProperty(ref _force, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public float Timeout
        {
            get => _timeout;
            set
            {
                if (SetProperty(ref _timeout, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public LocatorFillBotActionViewModel(LocatorFillBotAction action)
            : base(action)
        {
            _value = action.Value;
            _force = action.Options.Force;
            _timeout = action.Options.Timeout;
        }

        protected override void CancelChangesCore()
        {
            Value = Action.Value;
            Force = Action.Options.Force;
            Timeout = Action.Options.Timeout;
        }

        protected override bool CheckModified()
        {
            return !(_value == Action.Value
                && _force == Action.Options.Force
                && _timeout == Action.Options.Timeout);
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Action.Value = _value;
            Action.Options.Force = _force;
            Action.Options.Timeout = _timeout;
        }
    }
}
