using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using Playhouse.Domain;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class VariableViewModel : EditableViewModel
    {
        private string _name;
        private string _value;

        internal Variable Variable { get; }

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

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
        
        public VariableViewModel(Variable variable)
        {
            ArgumentNullException.ThrowIfNull(variable);

            Variable = variable;
            _name = Variable.Name;
            _value = Variable.Value;
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Variable.Name = _name;
            Variable.Value = _value;
        }

        protected override void CancelChangesCore()
        {
            Name = Variable.Name;
            Value = Variable.Value;
        }

        protected override bool CheckModified()
        {
            return !(_name == Variable.Name
                && _value == Variable.Value);
        }
    }
}
