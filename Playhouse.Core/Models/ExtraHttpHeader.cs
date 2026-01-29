using CommunityToolkit.Mvvm.ComponentModel;

namespace Playhouse.Core.Models
{
    public sealed class ExtraHTTPHeader : ObservableObject
    {
        //TODO: узнать че кого с генерацией строк у DataGrid в Avalonia.
        public string Header
        {
            get => field;
            set => SetProperty(ref field, value);
        }
        public string Value
        {
            get => field;
            set => SetProperty(ref field, value);
        }

        public ExtraHTTPHeader() { }

        public ExtraHTTPHeader(string header, string value)
        {
            ArgumentNullException.ThrowIfNull(header, nameof(header));
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            Header = header;
            Value = value;
        }
    }
}
