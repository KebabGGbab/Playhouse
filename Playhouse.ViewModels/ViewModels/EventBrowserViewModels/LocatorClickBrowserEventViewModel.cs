using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.ViewModels.EventArguments;
using Playhouse.ViewModels.ViewModels.PlaywrightViewModels;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class LocatorClickBrowserEventViewModel : BrowserEventViewModel
    {
        protected new LocatorClickBrowserEvent Event => (LocatorClickBrowserEvent)base.Event;

        public MouseButton Button
        {
            get => Event.ClickOptions.Button;
            set => SetProperty(Event.ClickOptions.Button, value, Event, (m, v) => m.ClickOptions.Button = v);
        }

        public int ClickCount
        {
            get => Event.ClickOptions.ClickCount;
            set => SetProperty(Event.ClickOptions.ClickCount, value, Event, (m, v) => m.ClickOptions.ClickCount = v);
        }

        public IEnumerable<KeyboardModifier>? Modifiers => Event.ClickOptions.Modifiers;

        public PositionViewModel Position { get; }

        public float Delay
        {
            get => Event.ClickOptions.Delay;
            set => SetProperty(Event.ClickOptions.Delay, value, Event, (m, v) => m.ClickOptions.Delay = v);
        }

        public bool Force
        {
            get => Event.ClickOptions.Force;
            set => SetProperty(Event.ClickOptions.Force, value, Event, (m, v) => m.ClickOptions.Force = v);
        }

        public int Steps
        {
            get => Event.ClickOptions.Steps;
            set => SetProperty(Event.ClickOptions.Steps, value, Event, (m, v) => m.ClickOptions.Steps = v);
        }

        public float Timeout
        {
            get => Event.ClickOptions.Timeout;
            set => SetProperty(Event.ClickOptions.Timeout, value, Event, (m, v) => m.ClickOptions.Timeout = v);
        }

        public bool Trial
        {
            get => Event.ClickOptions.Trial;
            set => SetProperty(Event.ClickOptions.Trial, value, Event, (m, v) => m.ClickOptions.Trial = v);
        }

        public LocatorClickBrowserEventViewModel(LocatorClickBrowserEvent @event) : base(@event)
        {
            Position = new PositionViewModel(@event.ClickOptions.Position);
            Position.PositionChanged += PositionChanged;
        }

        private void PositionChanged(PositionViewModel sender, PositionChangedEventArgs e)
        {
            Event.ClickOptions.Position = e.NewPosition;
        }
    }
}
