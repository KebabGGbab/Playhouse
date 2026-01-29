namespace Playhouse.ViewModels.Messages
{
    public class CollectionChangeMessage<T>
    {
        public T Item { get; }

        public CollectionChangeAction Action { get; }

        public CollectionChangeMessage(T item, CollectionChangeAction action)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            if (!Enum.IsDefined(action))
            {
                throw new ArgumentOutOfRangeException(nameof(action));
            }


            Item = item;
            Action = action;
        }
    }
}
