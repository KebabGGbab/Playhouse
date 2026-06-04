using CommunityToolkit.Mvvm.ComponentModel;

namespace Playhouse.ViewModels.ViewModels.Abstractions
{
    public class SelectableItemViewModel<T> : ObservableObject
    {
        public bool IsSelected
        {
            get;
            set => SetProperty(ref field, value);
        }

        public T Item { get; }

        public SelectableItemViewModel(T item)
            : this(item, false)
        {
        }

        public SelectableItemViewModel(T item, bool isSelected)
        {
            ArgumentNullException.ThrowIfNull(item);

            Item = item;
            IsSelected = isSelected;
        }
    }
}
