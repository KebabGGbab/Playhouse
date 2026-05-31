using Microsoft.Playwright;
using Playhouse.Core.Models.BrowserEvents;

namespace Playhouse.ViewModels.ViewModels.EventBrowserViewModels
{
    public class PageGoToBrowserEventViewModel : BrowserEventViewModel<PageGoToBrowserEvent>
    {
        public static IReadOnlyList<string> WaitUntilStates { get; } = Enum.GetValues<WaitUntilState>()
            .Select(w => w.ToString())
            .ToList()
            .AsReadOnly();

        private string _url;
        private string? _referer;
        private float _timeout;
        private string _waitUntil;

        public string Url
        {
            get => _url;
            set
            {
                if (SetProperty(ref _url, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public string? Referer
        {
            get => _referer;
            set
            {
                if (SetProperty(ref _referer, value))
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

        public string WaitUntil
        {
            get => _waitUntil;
            set
            {
                if (SetProperty(ref _waitUntil, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public PageGoToBrowserEventViewModel(PageGoToBrowserEvent @event)
            : base(@event)
        {
            _url = @event.Url;
            _referer = @event.Options.Referer;
            _timeout = @event.Options.Timeout;
            _waitUntil = @event.Options.WaitUntil.ToString();
        }

        protected override bool CheckModified()
        {
            return !(_url == Event.Url
                && _referer == Event.Options.Referer
                && _timeout == Event.Options.Timeout
                && _waitUntil == Event.Options.WaitUntil.ToString());
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Event.Url = _url;
            Event.Options.Referer = _referer;
            Event.Options.Timeout = _timeout;
            Event.Options.WaitUntil = Enum.Parse<WaitUntilState>(_waitUntil);
        }

        protected override void CancelChangesCore()
        {
            Url = Event.Url;
            Referer = Event.Options.Referer;
            Timeout = Event.Options.Timeout;
            WaitUntil = Event.Options.WaitUntil.ToString();
        }
    }
}
