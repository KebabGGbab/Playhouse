using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Microsoft.Playwright;
using Playhouse.Core.Models.BotActions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public class LocatorClickBotActionViewModel : BotActionViewModel<LocatorClickBotAction>
    {
        public static IReadOnlyList<string> Buttons { get; } = Enum.GetValues<MouseButton>()
            .Select(x => x.ToString())
            .ToList()
            .AsReadOnly();

        public static IReadOnlyList<string> Modifiers { get; } = Enum.GetValues<KeyboardModifier>()
            .Select(x => x.ToString())
            .ToList()
            .AsReadOnly();

        private readonly ObservableCollection<string> _selectedModofiers;

        private string _selectedButton;
        private int _clickCount;
        private float _delay;
        private bool _force;
        private int _steps;
        private float _timeout;
        private bool _trial;

        public string SelectedButton
        {
            get => _selectedButton;
            set
            {
                if (SetProperty(ref _selectedButton, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public int ClickCount
        {
            get => _clickCount;
            set
            {
                if (SetProperty(ref _clickCount, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public ReadOnlyObservableCollection<string> SelectedModifiers { get; }

        public float Delay
        {
            get => _delay;
            set
            {
                if (SetProperty(ref _delay, value))
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

        public int Steps
        {
            get => _steps;
            set
            {
                if (SetProperty(ref _steps, value))
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

        public bool Trial
        {
            get => _trial;
            set
            {
                if (SetProperty(ref _trial, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public IRelayCommand<string> AddModifierCommand;

        public IRelayCommand<string> RemoveModifierCommand;

        public LocatorClickBotActionViewModel(LocatorClickBotAction action) 
            : base(action)
        {
            AddModifierCommand = new RelayCommand<string>(AddModifier);
            RemoveModifierCommand = new RelayCommand<string>(RemoveModifier);
            _selectedButton = action.Options.Button.ToString();
            _clickCount = action.Options.ClickCount;
            _selectedModofiers = new ObservableCollection<string>(action.Options.Modifiers.Select(x => x.ToString()));
            SelectedModifiers = new ReadOnlyObservableCollection<string>(_selectedModofiers);
            _delay = action.Options.Delay;
            _force = action.Options.Force;
            _steps = action.Options.Steps;
            _timeout = action.Options.Timeout;
            _trial = action.Options.Trial;
        }

        private void AddModifier(string? modifier)
        {
            if (!CanAddModifier(modifier))
            {
                return;
            }

            _selectedModofiers.Add(modifier);
            IsModified = CheckModified();
        }

        private bool CanAddModifier([NotNullWhen(true)] string? modifier) =>
            modifier != null
            && !_selectedModofiers.Contains(modifier)
            && Modifiers.Contains(modifier);

        private void RemoveModifier(string? modifier)
        {
            if (!CanRemoveModifier(modifier))
            {
                return;
            }

            _selectedModofiers.Remove(modifier);
            IsModified = CheckModified();
        }

        private bool CanRemoveModifier([NotNullWhen(true)] string? modifier) =>
            modifier != null
            && _selectedModofiers.Contains(modifier);

        protected override bool CheckModified()
        {
            return !(_selectedButton == Action.Options.Button.ToString()
                && _clickCount == Action.Options.ClickCount
                && SelectedModifiers.Select(Enum.Parse<KeyboardModifier>).OrderDescending().SequenceEqual(Action.Options.Modifiers.OrderDescending())
                && _delay == Action.Options.Delay
                && _force == Action.Options.Force
                && _steps == Action.Options.Steps
                && _timeout == Action.Options.Timeout
                && _trial == Action.Options.Trial);
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Action.Options.Button = Enum.Parse<MouseButton>(_selectedButton);
            Action.Options.ClickCount = _clickCount;
            foreach (KeyboardModifier modifier in SelectedModifiers.Select(Enum.Parse<KeyboardModifier>))
            {
                Action.Options.Modifiers.Add(modifier);
            }
            foreach (KeyboardModifier modifier in Modifiers.Except(_selectedModofiers).Select(Enum.Parse<KeyboardModifier>))
            {
                Action.Options.Modifiers.Remove(modifier);
            }
            Action.Options.Delay = _delay;
            Action.Options.Force = _force;
            Action.Options.Steps = _steps;
            Action.Options.Timeout = _timeout;
            Action.Options.Trial = _trial;
        }

        protected override void CancelChangesCore()
        {
            SelectedButton = Action.Options.Button.ToString();
            ClickCount = Action.Options.ClickCount;
            _selectedModofiers.Clear();
            _selectedModofiers.AddRange(Action.Options.Modifiers.Select(m => m.ToString()));
            Delay = Action.Options.Delay;
            Force = Action.Options.Force;
            Steps = Action.Options.Steps;
            Timeout = Action.Options.Timeout;
            Trial = Action.Options.Trial;
        }
    }
}
