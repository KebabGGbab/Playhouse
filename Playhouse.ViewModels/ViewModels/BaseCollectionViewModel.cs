using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Playhouse.ViewModels.Messages;

namespace Playhouse.ViewModels.ViewModels
{
    public abstract class BaseCollectionViewModel<T> : ObservableObject
    {
        protected static void SendMessageAddItem(T item)
        {
            SendMessageChangedCollection(item, CollectionChangeAction.Add);
        }

        protected static void SendMessageRemoveItem(T item)
        {
            SendMessageChangedCollection(item, CollectionChangeAction.Remove);
        }

        private static void SendMessageChangedCollection(T item, CollectionChangeAction action)
        {
            WeakReferenceMessenger.Default.Send(new CollectionChangeMessage<T>(item, action));
        }
    }
}
