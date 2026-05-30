using Playhouse.Settings.Application.DTO;
using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Application.Services.Settings
{
    public interface IApplicationSettingsService
    {
        string CurrentCulture { get; }

        string PathToData { get; }

        IEnumerable<string> BrowserTypes { get; }

        IEnumerable<string> BrowserChannels { get; }

        event EventHandler<IApplicationSettingsService, EventArgs>? SettingsChanged;

        Task LoadAsync(CancellationToken token = default);

        public Task<Result> SaveAsync(ApplicationSettingsDto settings, CancellationToken token = default);
    }
}
