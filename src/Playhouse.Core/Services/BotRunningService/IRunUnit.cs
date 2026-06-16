using Playhouse.Domain;

namespace Playhouse.Core.Services.BotRunningService
{
    public interface IRunUnit
    {
        BotConfiguration Bot { get; }

        BrowserConfiguration Browser { get; }

        UnitStatus Status { get; }

        event EventHandler<IRunUnit, EventArgs>? StatusChanged;
    }
}
