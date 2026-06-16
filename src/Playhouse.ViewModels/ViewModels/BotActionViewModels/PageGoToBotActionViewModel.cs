using Microsoft.Playwright;
using Playhouse.Domain.BotActions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public class PageGoToBotActionViewModel : BotActionViewModel<PageGoToBotAction>
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

        public PageGoToBotActionViewModel(PageGoToBotAction action)
            : base(action)
        {
            _url = action.Url;
            _referer = action.Options.Referer;
            _timeout = action.Options.Timeout;
            _waitUntil = action.Options.WaitUntil.ToString();
        }

        protected override bool CheckModified()
        {
            return !(_url == Action.Url
                && _referer == Action.Options.Referer
                && _timeout == Action.Options.Timeout
                && _waitUntil == Action.Options.WaitUntil.ToString());
        }

        protected override async Task SaveChangesCoreAsync()
        {
            Action.Url = _url;
            Action.Options.Referer = _referer;
            Action.Options.Timeout = _timeout;
            Action.Options.WaitUntil = Enum.Parse<WaitUntilState>(_waitUntil);
        }

        protected override void CancelChangesCore()
        {
            Url = Action.Url;
            Referer = Action.Options.Referer;
            Timeout = Action.Options.Timeout;
            WaitUntil = Action.Options.WaitUntil.ToString();
        }
    }
}
