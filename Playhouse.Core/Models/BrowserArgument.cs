using CommunityToolkit.Mvvm.ComponentModel;

namespace Playhouse.Core.Models
{
    public sealed class BrowserArgument : ObservableObject
    {
        public string Argument
        {
            get => field;
            set => SetProperty(ref field, value);
        } = string.Empty;

        public BrowserArgument() { }

        public BrowserArgument(string argument)
        {
            ArgumentNullException.ThrowIfNull(argument, nameof(argument));

            Argument = argument;
        }
    }
}
