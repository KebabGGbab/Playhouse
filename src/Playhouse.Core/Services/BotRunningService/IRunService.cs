using Playhouse.Core.Models;

namespace Playhouse.Core.Services.BotRunningService
{
    public interface IRunService
    {
        BotConfiguration Bot { get; }

        IReadOnlyList<IRunUnit> Units { get; }

        int Progress { get; }

        Task RunAsync();

        event EventHandler<IRunService, EventArgs>? ProgressChanged;

        event EventHandler<IRunService, EventArgs>? RunCompleted;
    }
}
