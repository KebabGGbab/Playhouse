using CommunityToolkit.Mvvm.ComponentModel;

namespace Playhouse.ViewModels.ViewModels.Abstractions
{
    public sealed class SelectableItem<T> : ObservableObject
    {
        public bool IsSelected
        {
            get;
            set => SetProperty(ref field, value);
        }

        public T Item { get; }

        public SelectableItem(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            Item = item;
        }

        public SelectableItem(T item, bool isSelected)
            : this(item)
        {
            IsSelected = isSelected;
        }
    }
}
