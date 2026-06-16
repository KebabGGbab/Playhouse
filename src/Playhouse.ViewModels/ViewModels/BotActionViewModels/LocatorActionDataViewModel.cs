using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using Microsoft.Playwright;
using Playhouse.Domain.BotActions.Abstractions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public class LocatorActionDataViewModel : EditableViewModel
    {
        public static IReadOnlyList<string> Actions { get; } = ActionTypes.List.Select(a => a.Name).ToList().AsReadOnly();

        public static IReadOnlyList<string> Roles { get; } = Enum.GetValues<AriaRole>()
            .Select(r => r.ToString())
            .ToList()
            .AsReadOnly();

        private readonly LocatorActionData _data;

        private string _action;
        private string _role;
        private string? _text;
        private string? _id;
        private string _selector;

        public string Action
        {
            get => _action;
            set
            {
                if (SetProperty(ref _action, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public string Role
        {
            get => _role;
            set
            {
                if (SetProperty(ref _role, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public string? Text
        {
            get => _text;
            set
            {
                if (SetProperty(ref _text, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public string? Id
        {
            get => _id;
            set
            {
                if (SetProperty(ref _id, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public string Selector
        {
            get => _selector;
            set
            {
                if (SetProperty(ref _selector, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public LocatorActionDataViewModel(LocatorActionData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            _data = data;
            _action = _data.Action.Name;
            _role = _data.Role.ToString()!;
            _text = _data.Text;
            _id = _data.Id;
            _selector = _data.Selector;
        }

        protected override void CancelChangesCore()
        {
            Action = _data.Action.Name;
            Role = _data.Role.ToString()!;
            Text = _data.Text;
            Id = _data.Id;
            Selector = _data.Selector;
        }

        protected override bool CheckModified()
        {
            return !(_action == _data.Action.Name
                && _role == _data.Role.ToString()
                && _text == _data.Text
                && _id == _data.Id
                && _selector == _data.Selector);
        }

        protected override bool CanSaveChanges() => IsModified;

        protected override async Task SaveChangesCoreAsync()
        {
            _data.Action = ActionTypes.FromName(_action, true);
            _data.Role = Enum.Parse<AriaRole>(_role, true);
            _data.Text = _text;
            _data.Id = _id;
            _data.Selector = _selector;
        }

        protected override bool CanCancelChanges() => IsModified;
    }
}
