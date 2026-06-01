namespace Playhouse.Core.Models.BotActions.Abstractions
{
    public abstract class BotAction
    {
        public int Id { get; private set; }

        public required BotConfiguration Bot { get; init; }

        public virtual void Accept(IBotActionVisitor visitor) { }
    }
}
