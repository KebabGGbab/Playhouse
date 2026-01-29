using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using Playhouse.Core.Enums;
using Playhouse.Core.Models.BrowserEvents.Abstractions;

namespace Playhouse.Core.Models
{
	public class BotInfo : ObservableObject
    {

        public int Id
        {
            get => field;
            set => SetProperty(ref field, value);
        }
        public string Name
        {
            get => field;
            set => SetProperty(ref field, value);
        } = string.Empty;
        public BrowserType Browser
        {
            get => field;
            set => SetProperty(ref field, value);
        }
        public IList<BrowserEvent> BrowserEvents
        {
            get => field;
            set => SetProperty(ref field, value);
        } = [];

        public BotInfo() { }

        public BotInfo(string name, int id, BrowserType browser)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            Name = name;
            Id = id;
            Browser = browser;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append('[');
            sb.Append(Id);
            sb.Append(']');
            sb.Append(new string(' ', 5));
            sb.Append(Name);

            return sb.ToString();
        }
    }
}
