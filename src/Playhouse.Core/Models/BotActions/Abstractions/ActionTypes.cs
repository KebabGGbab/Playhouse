using Ardalis.SmartEnum;

namespace Playhouse.Core.Models.BotActions.Abstractions
{
    public sealed class ActionTypes : SmartEnum<ActionTypes>
    {
        public static readonly ActionTypes Click = new("Click", 1);
        public static readonly ActionTypes Change = new("Change", 2);

        private ActionTypes(string name, int id)
            : base(name, id)
        {
        }
    }
}
