namespace Playhouse.Core.Models.BrowserEvents.Abstractions
{
    public abstract class BrowserEvent
    {
        public int Id { get; private set; }

        public required BotInfo BotInfo { get; init; }

        public virtual void Accept(IBrowserEventVisitor visitor) { }
    }
}
