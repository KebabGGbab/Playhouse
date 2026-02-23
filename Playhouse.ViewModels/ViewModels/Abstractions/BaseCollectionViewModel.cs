using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Playhouse.ViewModels.Messages;

namespace Playhouse.ViewModels.ViewModels.Abstractions
{
    public abstract class BaseCollectionViewModel<T> : ObservableObject
    {
        protected static void SendMessageAddItems(List<T> item)
        {
            SendMessageChangedCollection(item, CollectionChangedAction.Add);
        }

        protected static void SendMessageRemoveItems(List<T> item)
        {
            SendMessageChangedCollection(item, CollectionChangedAction.Remove);
        }

        private static void SendMessageChangedCollection(List<T> item, CollectionChangedAction action)
        {
            WeakReferenceMessenger.Default.Send(new CollectionChangedMessage<T>(item, action));
        }
    }
}
