namespace Playhouse.ViewModels.Messages
{
    public class CollectionChangedMessage<T>
    {
        public IReadOnlyList<T> Items { get; }

        public CollectionChangedAction Action { get; }

        public CollectionChangedMessage(IReadOnlyList<T> items, CollectionChangedAction action)
        {
            ArgumentNullException.ThrowIfNull(items);
            if (!Enum.IsDefined(action))
            {
                throw new ArgumentOutOfRangeException(nameof(action));
            }


            Items = items;
            Action = action;
        }
    }
}
