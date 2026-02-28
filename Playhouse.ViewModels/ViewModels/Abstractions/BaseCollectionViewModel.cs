using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Playhouse.ViewModels.Messages;

namespace Playhouse.ViewModels.ViewModels.Abstractions
{
    public abstract class BaseCollectionViewModel<T> : ObservableObject
    {
        protected static void SendMessageAddItems(IReadOnlyList<T> item)
        {
            SendMessageChangedCollection(item, CollectionChangedAction.Add);
        }

        protected static void SendMessageRemoveItems(IReadOnlyList<T> item)
        {
            SendMessageChangedCollection(item, CollectionChangedAction.Remove);
        }

        private static void SendMessageChangedCollection(IReadOnlyList<T> item, CollectionChangedAction action)
        {
            WeakReferenceMessenger.Default.Send(new CollectionChangedMessage<T>(item, action));
        }
    }
}
