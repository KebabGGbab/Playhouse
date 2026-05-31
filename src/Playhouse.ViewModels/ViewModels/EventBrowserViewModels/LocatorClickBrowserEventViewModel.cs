using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.ViewModels.ViewModels.PlaywrightViewModels;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class LocatorClickBrowserEventViewModel : BrowserEventViewModel<LocatorClickBrowserEvent>
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

        public PositionViewModel Position { get; }

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

        public LocatorClickBrowserEventViewModel(LocatorClickBrowserEvent @event) : base(@event)
        {
            AddModifierCommand = new RelayCommand<string>(AddModifier);
            RemoveModifierCommand = new RelayCommand<string>(RemoveModifier);
            _selectedButton = @event.Options.Button.ToString();
            _clickCount = @event.Options.ClickCount;
            _selectedModofiers = new ObservableCollection<string>(@event.Options.Modifiers.Select(x => x.ToString()));
            SelectedModifiers = new ReadOnlyObservableCollection<string>(_selectedModofiers);
            Position = new PositionViewModel(@event.Options.Position);
            _delay = @event.Options.Delay;
            _force = @event.Options.Force;
            _steps = @event.Options.Steps;
            _timeout = @event.Options.Timeout;
            _trial = @event.Options.Trial;
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
            return !(_selectedButton == Event.Options.Button.ToString()
                && _clickCount == Event.Options.ClickCount
                && SelectedModifiers.Select(Enum.Parse<KeyboardModifier>).OrderDescending().SequenceEqual(Event.Options.Modifiers.OrderDescending())
                && Position.IsModified == false
                && _delay == Event.Options.Delay
                && _force == Event.Options.Force
                && _steps == Event.Options.Steps
                && _timeout == Event.Options.Timeout
                && _trial == Event.Options.Trial);
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Event.Options.Button = Enum.Parse<MouseButton>(_selectedButton);
            Event.Options.ClickCount = _clickCount;
            foreach (KeyboardModifier modifier in SelectedModifiers.Select(Enum.Parse<KeyboardModifier>))
            {
                Event.Options.Modifiers.Add(modifier);
            }
            foreach (KeyboardModifier modifier in Modifiers.Except(_selectedModofiers).Select(Enum.Parse<KeyboardModifier>))
            {
                Event.Options.Modifiers.Remove(modifier);
            }
            Position.SaveChangesCommand.Execute(null);
            Event.Options.Delay = _delay;
            Event.Options.Force = _force;
            Event.Options.Steps = _steps;
            Event.Options.Timeout = _timeout;
            Event.Options.Trial = _trial;
        }

        protected override void CancelChangesCore()
        {
            SelectedButton = Event.Options.Button.ToString();
            ClickCount = Event.Options.ClickCount;
            _selectedModofiers.Clear();
            _selectedModofiers.AddRange(Event.Options.Modifiers.Select(m => m.ToString()));
            Position.CancelChangesCommand.Execute(null);
            Delay = Event.Options.Delay;
            Force = Event.Options.Force;
            Steps = Event.Options.Steps;
            Timeout = Event.Options.Timeout;
            Trial = Event.Options.Trial;
        }
    }
}
